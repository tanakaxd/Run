using UnityEngine;

public class Adult : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 50;
    public float jumpForce = 500.0f;
    public float forceMultiplier = 50.0f;

    private int health;
    private int maxHealth = 10;
    private int healthPerFood = 2;
    private int healthPerMissile = -3;
    private bool movingRight;
    private bool onGround;


    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        movingRight = true;
        SwitchDirection();
        Jump();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float xmodifier = movingRight ? 1 : -1;
        //float zmodifier = movingForward ? 1 : -1;

        float inTheAirModifier = onGround ? 1.0f : 0.3f;
        Vector3 moveForce = transform.right * speed * xmodifier*inTheAirModifier;
        rb.AddForce(forceMultiplier * (moveForce - rb.velocity));
    }

    private void SwitchDirection()
    {
        movingRight = !movingRight;
        float randomTime = Random.Range(0.5f, 2.0f);
        Invoke("SwitchDirection", randomTime);
    }

    private void Jump()
    {
        if (onGround)
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            float randomTime = Random.Range(3.0f, 10.0f);
            Invoke("Jump", randomTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onGround = false;

    }
    private void OnTriggerStay(Collider other)
    {
        onGround = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            ChangeHealth(healthPerMissile);
            //onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && health < maxHealth)
        {
            Destroy(other.gameObject);
            ChangeHealth(healthPerFood);
        }

        if (other.gameObject.CompareTag("Nest"))
        {
            Destroy(other.gameObject);
            SpawnManager.instance.SpawnFriend(transform.position - transform.forward * 4);
        }
        

            if (other.gameObject.CompareTag("Dollar"))
            {
                Destroy(other.gameObject);
                Engine.instance.UpdateMoney(50);

            }
        
    }


    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if (health <= 0)
        {
            SpawnManager.instance.friends.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}