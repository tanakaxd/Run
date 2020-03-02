using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 50.0f;
    public float xspeed = 10.0f;
    public float zspeed = 10.0f;
    public float jumpForce = 300.0f;
    public float forceMultiplier = 30.0f;

    //private float zBound = 20.0f;
    private int health;
    private int maxHealth = 10;
    private int healthPerFood = 2;
    private int healthPerMissile = -3;
    private int moneyPerDollar = 50;
    private float starDuration = 5.0f;
    private float starTime;
    private bool doubleJump = false;
    private bool evasion = false;
    private bool jumpedTwice = false;
    private bool thrust = false;
    private bool twins = false;
    private bool invincible = false;

    [SerializeField, NonEditable]
    private bool onGround;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        ItemManager.instance.LogAllItems();

        if (ItemManager.instance.HasItem("Agility Up")) forceMultiplier *= (float)1.2;
        if (ItemManager.instance.HasItem("Armor")) healthPerMissile += 1;
        if (ItemManager.instance.HasItem("Bulk Up"))
        {
            playerRb.mass += 20; transform.localScale = new Vector3(3, 3, 3); maxHealth *= 3;
        }
        if (ItemManager.instance.HasItem("Double Jump")) doubleJump = true;
        if (ItemManager.instance.HasItem("Evasion")) evasion = true;
        if (ItemManager.instance.HasItem("High Jump")) jumpForce *= (float)1.5;
        if (ItemManager.instance.HasItem("HP Up")) maxHealth=(int)(maxHealth*1.5);
        if (ItemManager.instance.HasItem("Speed Up")) speed*= (float)1.2;
        if (ItemManager.instance.HasItem("Thrust")) thrust = true;
        if (ItemManager.instance.HasItem("Twins")) twins = true;

        health = maxHealth;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
        Jump();
        //ConstrainPlayerPosition();
    }

    private void Update()
    {
    
    }

    private void MovePlayer()
    {
        //onGround = transform.position.y>Mathf.Epsilon?

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float inTheAirModifier = onGround ? 1.0f : 0.3f;
        Vector3 moveForce = new Vector3(horizontalInput, 0, verticalInput).normalized * speed * inTheAirModifier;

        playerRb.AddForce(forceMultiplier * (moveForce - playerRb.velocity));

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
            if (!thrust)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                playerRb.AddForce(Vector3.forward * jumpForce*2, ForceMode.Impulse);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !jumpedTwice)
        {
            if (!thrust)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                playerRb.AddForce(Vector3.forward * jumpForce*2, ForceMode.Impulse);
            }
            jumpedTwice = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            if(!invincible)ChangeHealth(healthPerMissile);
            //onGround = false;
            Debug.Log("Missile hit!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //enumが使えそう

        if (other.gameObject.CompareTag("Food") && health < maxHealth)
        {
            Destroy(other.gameObject);
            ChangeHealth(healthPerFood);
        }
        if (other.gameObject.CompareTag("Nest"))
        {
            Destroy(other.gameObject);
            SpawnManager.instance.SpawnFriend(transform.position + transform.forward * 2);
            if(twins) SpawnManager.instance.SpawnFriend(transform.position - transform.forward * 2);
        }
        if (other.gameObject.CompareTag("Dollar"))
        {
            Destroy(other.gameObject);
            Engine.instance.UpdateMoney(moneyPerDollar);

        }
        if (other.gameObject.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            StartCoroutine("Invincible");

        }
        if (other.gameObject.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
            playerRb.AddForce(Vector3.forward * jumpForce*2, ForceMode.Impulse);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        onGround = false;
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Boundary"))
        {
            onGround = true;
            //Debug.Log(collision.gameObject);
            jumpedTwice = false;


        }

    }

    
    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        //Debug.Log(health + " / " + maxHealth);
        UIHealthBar.instance.SetHealth((float)health / maxHealth);
        if (health <= 0)
        {
            if (!evasion)
            {
                Engine.instance.GameOver();
            }
            else
            {
                evasion = false;
                health = 1;
            }
        }
        Debug.Log("delta: " + amount + " → HP: " + health);
    }

    IEnumerator Invincible()
    {
        invincible = true;
        yield return new WaitForSeconds(starDuration);
        invincible = false;
    }

}