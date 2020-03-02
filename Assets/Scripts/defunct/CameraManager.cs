using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject missileCamera;

    private List<MissileController> missiles;
    private bool hasMissile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkMissile();
        if (hasMissile)
        {
            missileCamera.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
        }
        else
        {
            missileCamera.GetComponent<Camera>().enabled = false;
            mainCamera.GetComponent<Camera>().enabled = true;
        }
    }

   void checkMissile()
    {
        GameObject missile = GameObject.FindWithTag("Missile");
        hasMissile = missile != null ? true : false;
    }
}
