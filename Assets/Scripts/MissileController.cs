using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Camera mainCamera;
    private Camera missileCamera;

    private Rigidbody catMissileRb;

    private float speed = 1.0f;
    private float torque = 10.0f;
    private float xDestroy = 25;
    public bool Broken { get; set; }
    public bool InCamera { get; set; }

    private void Awake()
    {
        //Debug.Log("missile controller Awake() called");

        if (Random.Range(0, 100) < 1)
        {
            Broken = true;
        }
        if (Random.Range(0, 10) == 0)
        {
            InCamera = true;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        catMissileRb = GetComponent<Rigidbody>();
        //Debug.Log("missile controller Start() called");

        missileCamera = GameObject.Find("MissileCamera").GetComponent<Camera>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (InCamera && !Broken)
        {
            missileCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        catMissileRb.AddForce(transform.forward * speed, ForceMode.Impulse);

        if (Broken)
        {
            catMissileRb.AddTorque(transform.up * torque, ForceMode.Impulse);
        }
        else
        {
            catMissileRb.AddTorque(transform.forward * torque, ForceMode.Impulse);
        }

        if (transform.position.x > xDestroy || transform.position.x < -xDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()// Startより早い。awakeより遅い
    {
        //Debug.Log("missile enabled!");
    }

    private void OnDisable()
    {
        //Debug.Log("missile disabled!");
        missileCamera.enabled = false;
        mainCamera.enabled = true;
    }

    private void OnDestroy()
    {
        //Debug.Log("missile destroyed!");
    }
}