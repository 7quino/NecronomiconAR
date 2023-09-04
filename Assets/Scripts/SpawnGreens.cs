using UnityEngine;
using UnityEngine.UI;

public class SpawnGreens : MonoBehaviour
{
    public Toggle foodToggle;
    public GameObject carrotPrefab;
    public GameObject parsnipPrefab;

    void Start()
    {
        InputManager.Instance.onSpawnGreens.AddListener(SpawnGreen);
    }

    void SpawnGreen(Ray ray)
    {
        GameObject spawnedFood = Instantiate( WhichFood(), ray.origin, Quaternion.identity);
        spawnedFood.GetComponent<Rigidbody>().AddForce(ray.direction * 500);
        Destroy(spawnedFood, 20);
    }

    public GameObject WhichFood()
    {
        return foodToggle.isOn ? carrotPrefab : parsnipPrefab;
    }
}