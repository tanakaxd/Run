using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50.0f;
    public float xspeed = 10.0f;
    public float zspeed = 10.0f;
    public float jumpForce = 300.0f;
    public float forceMultiplier = 30.0f;

    private float zBound = 20.0f;
    private int health;
    private int maxHealth = 5;
    private int healthPerFood = 1;
    private int healthPerMissile = -3;
    [SerializeField, NonEditable]
    private bool onGround;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
        //ConstrainPlayerPosition();
    }

    private void MovePlayer()
    {
        
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float inTheAirModifier = onGround ? 1.0f : 0.3f;
        Vector3 moveForce = new Vector3(horizontalInput, 0, verticalInput).normalized * speed * inTheAirModifier;
     


        playerRb.AddForce(forceMultiplier*(moveForce - playerRb.velocity));

        

        //float x = horizontalInput * speed;
        //float z = verticalInput * speed;
        //playerRb.MovePosition(transform.position + new Vector3(x, 0, z) * Time.deltaTime);



        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
        
    }

    //private void ConstrainPlayerPosition()
    //{
    //    if (transform.position.z > zBound)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
    //    }
    //    else if (transform.position.z < -zBound)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
    //    }
    //}

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            ChangeHealth(healthPerMissile);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && health < 5)
        {
            Destroy(other.gameObject);
            ChangeHealth(healthPerFood);
        }

        if (other.gameObject.CompareTag("Nest"))
        {
            Destroy(other.gameObject);
            SpawnManager.instance.SpawnFriend(transform.position-transform.forward*4);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, 5);
        //Debug.Log(health + " / " + maxHealth);
        UIHealthBar.instance.SetHealth((float)health / maxHealth);
        if (health <= 0)
        {
            Engine.instance.GameOver();
        }
    }
}