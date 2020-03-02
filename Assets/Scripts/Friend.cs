using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public GameObject adultPrefab;

    //[SerializeField, NonEditable]
    private GameObject player;
    private Rigidbody playerRb;
    private Rigidbody rb;
    public float speed = 100.0f;

    private float timeToGrow = 15.0f;
    private float count;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player);
        count = timeToGrow;

        //playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0) Grow();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        target = player.transform.position;
        Vector3 desired = target - transform.position;
        Vector3 force = (desired - rb.velocity)*speed;
        rb.AddForce(force);
        //Debug.Log(target);
    }

    private void Grow()
    {
        SpawnManager.instance.friends.Add(Instantiate(adultPrefab, transform.position,transform.rotation));

        SpawnManager.instance.friends.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //enumが使えそう

        if (other.gameObject.CompareTag("Dollar"))
        {
            Destroy(other.gameObject);
            Engine.instance.UpdateMoney(50);

        }
    }
}
