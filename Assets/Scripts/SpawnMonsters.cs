using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnMonsters : MonoBehaviour
{
    public ARTrackedImageManager imageManager;
    public GameObject[] animalPrefabs;
    float spawnRangeX = 10;
    float spawnRangeY = 10;
    float spawnPosZ = 25;

    Coroutine spawnRoutine;
    float spawnInterval = 3f;
    float spawnIntervalReduction = 0.95f;
    public float monsterSpeed = 5.0f;
    float monsterSpeedIncrease = 0.1f;
    bool spawnStarted = false;

    public static SpawnMonsters instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        imageManager.trackedImagesChanged += AddGreenMonsterLitsener;
        NecronomiconGameManager.instance.onStartSpawnMonsters.AddListener(() => { StartCoroutine(SpawnRandomMonster()); });
        NecronomiconGameManager.instance.onStopSpawnMonsters.AddListener(() => { StopCoroutine(spawnRoutine); });
        NecronomiconGameManager.instance.onGameOver.AddListener(() => { Destroy(gameObject);  });
    }

    void AddGreenMonsterLitsener(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (spawnStarted) return;
        spawnStarted = true;

        GreenMonster.instance.onIntroFinnished.AddListener(() => { spawnRoutine = StartCoroutine(SpawnRandomMonster()); });
    }

    IEnumerator SpawnRandomMonster()
    {
        yield return new WaitForSeconds(spawnInterval);

        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
        
        spawnInterval *= spawnIntervalReduction;
        monsterSpeed += monsterSpeedIncrease;

        spawnRoutine = StartCoroutine(SpawnRandomMonster());
    }
}
