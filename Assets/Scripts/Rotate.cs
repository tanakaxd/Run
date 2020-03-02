using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    private float angularVelocity=90.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.rotation= Quaternion.Euler(0, angularVelocity*Time.fixedDeltaTime, 0) * rb.rotation;
    }
}
