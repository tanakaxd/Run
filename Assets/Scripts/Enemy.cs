using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3;
    private float baseSpeed = 3;
    private float moveRange = 6;
    private float fireRateRangeMin = 3;
    private float fireRateRangeMax = 3;
    private float deviationPerShots = 5.0f;
    private float respawnRate = 5.0f;
    private float frozenAfterRespawn = 1.5f;
    public int shotsPerFire { get; set; }

    public GameObject catMissilePrefab;
    private Rigidbody catRb;
    private MissileCameraController missileCam;

    public List<GameObject> friendsOnField = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        catRb = GetComponent<Rigidbody>();
        FireMissile();
        missileCam = GameObject.Find("MissileCamera").GetComponent<MissileCameraController>();
        shotsPerFire = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpDown();
    }


    
    
    void FireMissile()
    {
        //z軸方向へ進むという前提。だからforwardとなっている。

        friendsOnField = SpawnManager.instance.friends;
        //Debug.Log(friendsOnField.Count);
        if (friendsOnField.Count!=0)
        {
            int randomIndex = Random.Range(0, friendsOnField.Count);
            GameObject target = friendsOnField[randomIndex];
            if (target != null)
            {
                Vector3 toTarget = target.transform.position - transform.position;
                Vector3 newPos = transform.position + transform.right*2 + transform.up; //Enemyの前にくるように調整。軸が変
                for (int i = 0; i < shotsPerFire; i++)
                {
                    Quaternion angle = Quaternion.Euler(0,deviationPerShots*i,0) * Quaternion.LookRotation(toTarget);
                GameObject missile= Instantiate(catMissilePrefab, newPos+transform.forward*(-2)*i, angle) as GameObject;
                missileCam.Missile = missile;

                }
            }
        }

        float nextInterval = Random.Range(fireRateRangeMin, fireRateRangeMax);
        Invoke("FireMissile", nextInterval);
    }

    void MoveUpDown()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > moveRange || transform.position.z < -moveRange)
        {
            speed *= -1;
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
        Invoke("FireMissile", frozenAfterRespawn);

        //FireMissile();
    }

    private void OnDisable()
    {
        CancelInvoke("FireMissile");
        //何秒後かに再出現
        Invoke("Respawn", respawnRate);

    }

    private void OnEnable()
    {
        //Invoke("FireMissile", 1.0f);
    }
}
