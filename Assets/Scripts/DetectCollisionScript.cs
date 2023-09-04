using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionScript : MonoBehaviour
{
    public int pointsWhenDie = 1;

    public AudioSource audioSource;
    public ParticleSystem puffPS;
    public GameObject monsterMesh;
    public Collider monsterCollider;

    void Start()
    {
        monsterCollider = GetComponent<Collider>();
        NecronomiconGameManager.instance.onStopSpawnMonsters.AddListener(() => { StartCoroutine(DestroyMonster()); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BlueMonster") && other.gameObject.CompareTag("Parsnip"))
        {
            NecronomiconGameManager.instance.score += pointsWhenDie;
            Destroy(other.gameObject);
            monsterCollider.enabled = false;
            StartCoroutine(DestroyMonster());
        }

        if (gameObject.CompareTag("PurpleMonster") && other.gameObject.CompareTag("Carrot"))
        {
            NecronomiconGameManager.instance.score += pointsWhenDie;
            monsterCollider.enabled = false;
            Destroy(other.gameObject);
            StartCoroutine(DestroyMonster());
        }

        if (other.gameObject.CompareTag("MainCamera"))
        {
            NecronomiconGameManager.instance.OnLivesLost();
            monsterCollider.enabled = false;
            StartCoroutine(DestroyMonster());
        }
    }

    IEnumerator DestroyMonster()
    {
        audioSource.Play();
        puffPS.Play();
        Destroy(monsterMesh);
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
