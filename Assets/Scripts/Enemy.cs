using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3;
    private float baseSpeed = 3;
    private float moveRange = 6;
    private float fireRateRangeMin = 2;
    private float fireRateRangeMax = 3;

    private Rigidbody catRb;
    public GameObject catMissilePrefab;
    private MissileCameraController missileCam;

    public GameObject[] friendsOnField;
    // Start is called before the first frame update
    void Start()
    {
        catRb = GetComponent<Rigidbody>();
        StartCoroutine(FireMissileAgain());
        //InvokeRepeating("FireMissile", 1, fireRateRangeMax);
        missileCam = GameObject.Find("MissileCamera").GetComponent<MissileCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpDown();
    }

    void RotateXToTarget()
    {
        //target one of the mice
        friendsOnField = GameObject.FindGameObjectsWithTag("Friend");
        int randomIndex = Random.Range(0, friendsOnField.Length);
        GameObject target = friendsOnField[randomIndex];

        //rotate around X axis to target and fire
        //Vector3 toTarget = target.transform.position - transform.position;
        //transform.rotation = Quaternion.FromToRotation(transform.up, toTarget);
    }

    IEnumerator FireMissileAgain()
    {
        float nextInterval = Random.Range(fireRateRangeMin, fireRateRangeMax);
        yield return new WaitForSeconds(nextInterval);
        FireMissile();
        StartCoroutine(FireMissileAgain());

    }
    
    
    void FireMissile()
    {
        //z軸方向へ進むという前提。だからforwardとなっている。
        //RotateXToTarget();
        friendsOnField = GameObject.FindGameObjectsWithTag("Friend");
        int randomIndex = Random.Range(0, friendsOnField.Length);
        GameObject target = friendsOnField[randomIndex];
        Vector3 toTarget = target.transform.position - transform.position;
        GameObject missile= Instantiate(catMissilePrefab, transform.position,Quaternion.LookRotation(toTarget));
        missileCam.Missile = missile;


        //reset rotation
        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoveUpDownRb()
    {
        //moving up and down
        catRb.AddForce(Vector3.forward * speed, ForceMode.Impulse);

        //switching moving direction
        if (transform.position.z > moveRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, moveRange);
            speed = baseSpeed * -1;

            //catRb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        }
        else if (transform.position.z < -moveRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -moveRange);
            speed = baseSpeed;
            //catRb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        }
    }
    void MoveUpDown()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z > moveRange)
        {
            speed = baseSpeed * -1;
        }
        else if (transform.position.z < -moveRange)
        {
            speed = baseSpeed;
        }
    }
}
