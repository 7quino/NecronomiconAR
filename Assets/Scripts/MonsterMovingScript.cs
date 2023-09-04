using UnityEngine;

public class MonsterMovingScript : MonoBehaviour
{
    private float speed;
    public float rotatiuonDamping = 4f;
    public Transform arCameraPosition;
    public SpawnMonsters spawnManager;

    void Start()
    {
        GameObject arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        arCameraPosition = arCamera.GetComponent<Transform>();
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnMonsters>();
        speed = SpawnMonsters.instance.monsterSpeed;
    }

    void Update()
    {
        var rotatation = Quaternion.LookRotation(arCameraPosition.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotatation, Time.deltaTime * rotatiuonDamping);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, arCameraPosition.position, step);
    }
}
