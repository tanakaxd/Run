using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public GameObject playerPrefab;
    public GameObject foodPrefab;
    public GameObject nestPrefab;
    public GameObject friendPrefab;
    public GameObject stonewallPrefab;
    public GameObject rockPrefab;
    public GameObject dollarPrefab;
    public GameObject starPrefab;
    public GameObject firePrefab;

    [HideInInspector]
    public List<GameObject> friends = new List<GameObject>();

    private float xSpawnRange = 15;
    private float zSpawnRange = 10;
    private float zSpawnPos = 40;

    private float foodSpawnInterval = 4;
    private float nestSpawnInterval = 7;
    private float rockSpawnInterval = 5;
    private float stonewallSpawnInterval = 8;
    private float dollarSpawnInterval = 3;
    private float starSpawnInterval = 15;
    private float fireSpawnInterval = 11;
    private float startDelay = 1;
    private int childrenOnStart = 1;

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
        if (ItemManager.instance.HasItem("Far Sight")) zSpawnPos += 20;


        InvokeRepeating("SpawnFood", startDelay, foodSpawnInterval);
        InvokeRepeating("SpawnNest", startDelay, nestSpawnInterval);
        InvokeRepeating("SpawnRock", startDelay, rockSpawnInterval);
        InvokeRepeating("SpawnStoneWall", startDelay, stonewallSpawnInterval);
        InvokeRepeating("SpawnDollar", startDelay, dollarSpawnInterval);
        InvokeRepeating("SpawnStar", startDelay, starSpawnInterval);
        InvokeRepeating("SpawnFire", startDelay, fireSpawnInterval);

        //SpawnThings(foodPrefab, zSpawnPos, startDelay, foodSpawnInterval);
        //SpawnThings(nestPrefab, zSpawnPos, startDelay, nestSpawnInterval);
        //SpawnThings(rockPrefab, zSpawnPos, startDelay, rockSpawnInterval);
        //SpawnThings(stoneWallPrefab, zSpawnPos, startDelay, stoneWallSpawnInterval);

        friends.Add(Instantiate(playerPrefab));

        for (int i = 0; i < childrenOnStart; i++)
        {
            SpawnFriend(GenerateRandomPosition());
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    //private void SpawnThings(GameObject prefab, float zPos, float delay, float interval)
    //{
    //    void Spawn()
    //    {
    //        Instantiate(prefab, GenerateRandomPosition(zPos), prefab.transform.rotation);
    //    }
    //    InvokeRepeating("Spawn", delay, interval);

    //}

    //private void SpawnThings(GameObject prefab, float delay, float interval)
    //{
    //    void Spawn()
    //    {
    //        Instantiate(prefab, GenerateRandomPosition(), prefab.transform.rotation);
    //    }
    //    InvokeRepeating("Spawn", delay, interval);

    //}

    private void SpawnFood()
    {
        Instantiate(foodPrefab, GenerateRandomPosition(zSpawnPos), foodPrefab.transform.rotation);
    }

    private void SpawnNest()
    {
        Instantiate(nestPrefab, GenerateRandomPosition(zSpawnPos), nestPrefab.transform.rotation);
    }

    private void SpawnRock()
    {
        Instantiate(rockPrefab, GenerateRandomPosition(zSpawnPos)+ rockPrefab.transform.position, rockPrefab.transform.rotation);
    }

    private void SpawnStoneWall()
    {
        Instantiate(stonewallPrefab, GenerateRandomPosition(zSpawnPos)+stonewallPrefab.transform.position, stonewallPrefab.transform.rotation);
    }

    private void SpawnDollar()
    {
        Instantiate(dollarPrefab, GenerateRandomPosition(zSpawnPos) + dollarPrefab.transform.position, dollarPrefab.transform.rotation);
    }

    private void SpawnStar()
    {
        Instantiate(starPrefab, GenerateRandomPosition(zSpawnPos) + starPrefab.transform.position, starPrefab.transform.rotation);
    }

    private void SpawnFire()
    {
        Instantiate(firePrefab, GenerateRandomPosition(zSpawnPos) + firePrefab.transform.position, firePrefab.transform.rotation);
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