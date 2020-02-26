using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject foodPrefab;
    public GameObject nestPrefab;
    public GameObject friendPrefab;

   // public GameObject[] friends;
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

        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
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
    void Update()
    {
    
    }

    void SpawnFood()
    {
        Instantiate(foodPrefab, GenerateRandomPosition(), foodPrefab.transform.rotation);
    }

    void SpawnNest()
    {
        Instantiate(nestPrefab, GenerateRandomPosition(), nestPrefab.transform.rotation);

    }

    public void SpawnFriend(Vector3 pos)
    {
        friends.Add(Instantiate(friendPrefab, pos, friendPrefab.transform.rotation)as GameObject);
    }

    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        //float randomZ = Random.Range(-zSpawnRange, zSpawnRange);

        return new Vector3(randomX, 0, 20);

    }
}
