using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject friendPrefab;

    public float speed = 15.0f;
    public float jumpForce = 100.0f;

    private float zBound = 20.0f;
    private int health;
    private int maxHealth = 5;
    private int healthPerFood=1;
    private int healthPerMissile=-3;
    private bool isJumping;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

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

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Missile"))
        {
            //Debug.Log("crashed into the enemy");
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
            //Instantiate(friendPrefab, transform.position, transform.rotation);
            SpawnManager.instance.SpawnFriend(transform.position);
        }
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, 5);
        //Debug.Log(health + " / " + maxHealth);
        UIHealthBar.instance.SetHealth((float)health / maxHealth);
    }
}
