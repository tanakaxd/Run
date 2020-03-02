using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 20.0f;
    private float maxRange = 70.0f;
    private float sqrmaxRange;
    Vector3 center = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrmaxRange = maxRange * maxRange;
    }

    private void FixedUpdate()
    {
        //transform.Translate(Vector3.back * speed * Time.deltaTime);
        rb.MovePosition(transform.position + Vector3.back * speed * Time.fixedDeltaTime);
        float sqrDistance = (transform.position - center).sqrMagnitude;
        if (sqrDistance > sqrmaxRange)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Boundary"))
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("boundary");
    //    }

    //}

}
