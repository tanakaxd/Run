using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private float speed = 20.0f;
    private float maxRange = 50.0f;
    private float sqrmaxRange;
    Vector3 center = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        sqrmaxRange = maxRange * maxRange;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        float sqrDistance = (transform.position - center).sqrMagnitude;
        if (sqrDistance > sqrmaxRange)
        {
            Destroy(gameObject);
        }
    }
}
