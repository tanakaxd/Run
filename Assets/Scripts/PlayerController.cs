﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject friendPrefab;

    public float speed = 10.0f;
    private float zBound = 7;
    private int health;
    private int maxHealth = 5;
    private int healthPerFood=1;
    private int healthPerMissile=-3;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();

    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //playerRb.AddForce(Vector3.right * speed * horizontalInput);
        //playerRb.AddForce(Vector3.forward * speed * verticalInput);

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

    }

    void ConstrainPlayerPosition()
    {
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Missile"))
        {
            Debug.Log("crashed into the enemy");
            ChangeHealth(healthPerMissile);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food")&&health<5)
        {
            Destroy(other.gameObject);
            ChangeHealth(healthPerFood);
        }

        if (other.gameObject.CompareTag("Nest"))
        {
            Destroy(other.gameObject);
            Instantiate(friendPrefab, transform.position, transform.rotation);
        }
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, 5);
        Debug.Log(health + " / " + maxHealth);
        UIHealthBar.instance.SetHealth((float)health / maxHealth);
    }
}
