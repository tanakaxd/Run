using System.Collections;
using UnityEngine;

public class Detonate : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

   IEnumerator DetonateSelf()
    {
        SphereCollider blast = GetComponent<SphereCollider>();
        blast.enabled = true;
        yield return null;
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            SpawnManager.instance.friends.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Missile"))
        {
        }

        StartCoroutine("DetonateSelf");


    }
}