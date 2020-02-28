using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public Engine engine;
    // Start is called before the first frame update
    void Awake()
    {
        if (Engine.instance == null)
        {
            Instantiate(engine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
