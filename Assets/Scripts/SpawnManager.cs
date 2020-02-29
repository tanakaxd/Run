using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject foodPrefab;
    public GameObject nestPrefab;
    public GameObject friendPrefab;

    [HideInInspector]
    public List<GameObject> friends = new List<GameObject>();

    private float xSpawnRange = 15;
    private float zSpawnRange = 6;

    private float foodSpawnInterval = 4;
    private float nestSpawnInterval = 10;
    private float startDelay = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnFood", startDelay, foodSpawnInterval);
        InvokeRepeating("SpawnNest", startDelay, nestSpawnInterval);

        friends.Add(GameObject.Find("Player"));

        for (int i = 0; i < 5; i++)
        {
            SpawnFriend(GenerateRandomPosition());
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void SpawnFood()
    {
        Instantiate(foodPrefab, GenerateRandomPosition(30.0f), foodPrefab.transform.rotation);
    }

    private void SpawnNest()
    {
        Instantiate(nestPrefab, GenerateRandomPosition(30.0f), nestPrefab.transform.rotation);
    }

    public void SpawnFriend(Vector3 pos)
    {
        friends.Add(Instantiate(friendPrefab, pos, friendPrefab.transform.rotation) as GameObject);
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        float randomZ = Random.Range(-zSpawnRange, zSpawnRange);

        return new Vector3(randomX, 0, randomZ);
    }

    private Vector3 GenerateRandomPosition(float zPos)
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        //float randomZ = Random.Range(-zSpawnRange, zSpawnRange);

        return new Vector3(randomX, 0, zPos);
    }
}