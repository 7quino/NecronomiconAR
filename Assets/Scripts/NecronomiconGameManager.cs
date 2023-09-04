using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class NecronomiconGameManager : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public AudioSource backgroundAudio;
    public GameObject gameUI;
    public GameObject[] heartLifesUI;
    public GameObject lifeLost;
    public GameObject gameOver;
    public TextMeshProUGUI finalPoints;
    public int lives = 3;
    public int score = 0;

    public UnityEvent onStopSpawnMonsters = new UnityEvent();
    public UnityEvent onStartSpawnMonsters = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public static NecronomiconGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lifeLost.SetActive(false);
        gameOver.SetActive(false);
        imageManager.trackedImagesChanged += (ARTrackedImagesChangedEventArgs eventArg) => { GreenMonster.instance.onIntroFinnished.AddListener(StartGame); };
    }

    void StartGame()
    {
        backgroundAudio.Play();
    }

    public void OnLivesLost()
    {
        lives--;
        switch (lives)
        {
            case 2:
                {
                    heartLifesUI[0].SetActive(false);
                    StartCoroutine(LifeLost());
                    break;
                }
            case 1:
                {
                    heartLifesUI[1].SetActive(false);
                    StartCoroutine(LifeLost());
                    break;
                }
            case 0:
                {
                    heartLifesUI[2].SetActive(false);
                    StartCoroutine(GameOver());
                    break;
                }
        }
    }

    private IEnumerator LifeLost()
    {
        onStopSpawnMonsters.Invoke();
        lifeLost.SetActive(true);
        yield return new WaitForSeconds(2f);
        lifeLost.SetActive(false);
        onStartSpawnMonsters.Invoke();
    }

    private IEnumerator GameOver()
    {
        onStopSpawnMonsters.Invoke();
        onGameOver.Invoke();
        lifeLost.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        lifeLost.SetActive(false);

        gameOver.SetActive(true);
        finalPoints.text = "Total feeds: " + score;
    }
}
