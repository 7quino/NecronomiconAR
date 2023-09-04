using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GreenMonster : MonoBehaviour
{
    //Variables input
    int touchCount = 0;

    //Variables monster actions
    public Animator animator;
    public GameObject sleepingPS;
    public ParticleSystem smokePS;
    public GameObject[] destroyedInSmoke;

    //Sounds
    public AudioSource audioSource;
    public AudioClip sleepingSound;
    public AudioClip puffSound;

    //Variables text
    public float charPrintDelay = 0f;
    public GameObject[] chatbubbles;
    public List<IntroMessage> introMessages;
    char[] textEnteredSplit;
    bool isPrinting = false;

    public UnityEvent onIntroFinnished = new UnityEvent();
    public static GreenMonster instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (GameObject bubble in chatbubbles) bubble.SetActive(false);
        InputManager.Instance.onGreenMonsterPressed.AddListener(IntroSequense);
        SleepingMonster();
    }

    void SleepingMonster()
    {
        audioSource.clip = sleepingSound;
        audioSource.Play();
    }

    void IntroSequense()
    {
        if (isPrinting || touchCount >= introMessages.Count) return;

        foreach (GameObject bubble in chatbubbles) bubble.SetActive(false);

        switch (touchCount)
        {
            case 0:
                sleepingPS.SetActive(false);
                animator.SetTrigger("WakeUpTrigger");
                break;
            case 5:
                StartCoroutine(GoingUpInSmoke(touchCount));
                break;
        }

        StartCoroutine(PlaySound(touchCount));
        StartCoroutine(PrintText(touchCount));
        touchCount++;
    }


    IEnumerator PrintText(int textCount)
    {
        isPrinting = true;

        introMessages[textCount].chatBubble.SetActive(true);
        introMessages[textCount].textObject.text = string.Empty;
        textEnteredSplit = introMessages[textCount].message.ToCharArray();

        for (int i = 0; i < textEnteredSplit.Length; i++)
        {
            yield return new WaitForSeconds(charPrintDelay);
            introMessages[textCount].textObject.text += textEnteredSplit[i];
        }

        isPrinting = false;
    }

    IEnumerator PlaySound(int textCount)
    {
        if (textCount != 0) animator.SetTrigger("TalkingTrigger");

        audioSource.clip = introMessages[textCount].clip;
        audioSource.PlayOneShot(introMessages[textCount].clip);
        yield return new WaitForSeconds(introMessages[textCount].clip.length);

        if (textCount != 0) animator.SetTrigger("AwakeTrigger");
    }


    IEnumerator GoingUpInSmoke(int textCount)
    {
        yield return new WaitForSeconds(introMessages[textCount].clip.length);

        audioSource.PlayOneShot(puffSound);
        smokePS.Play();

        yield return new WaitForSeconds(1f);

        onIntroFinnished.Invoke();
        foreach (GameObject go in destroyedInSmoke) Destroy(go);
        Destroy(gameObject, 3);
    }
}


[System.Serializable]
public class IntroMessage
{
    public String message;
    public GameObject chatBubble;
    public TextMeshProUGUI textObject;
    public AudioClip clip;
}



