﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private float speed = 20.0f;
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z <= -50)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
