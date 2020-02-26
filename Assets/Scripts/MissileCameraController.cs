using UnityEngine;

public class MissileCameraController : MonoBehaviour
{
    //このクラスは追従するかしないかだけ。cameraのon offはmissile controllerが決める
    private GameObject missile;
    private Transform missileHead;
    private MissileController missileController;
    private bool isFollowing = false;

    public GameObject Missile
    {
        get { return missile; }
        set
        {
            missile = value;
            Debug.Log("missile assigned");
            missileController = missile.GetComponent<MissileController>();

            if ((!missileController.Broken) && (missileController.InCamera))
            {
                modifiedOffset = missile.transform.TransformDirection(offset);
                missileHead = missile.transform.Find("Missile Head");
                isFollowing = true;
            }
            else
            {
            isFollowing = false;
            }
        }
    }

    // private Vector3 offset = new Vector3(3.7f,1.2f,-2.3f);
    private Vector3 offset = new Vector3(0.54f, -0.57f, -0.64f); //カメラを一度子要素にして得たミサイルに対する相対的な位置

    private Vector3 modifiedOffset;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if ((missile != null) && isFollowing)
        {
            Vector3 pos = missile.transform.position + modifiedOffset;
            //Vector3 pos = missile.transform.position +offset;
            transform.position = pos;
            transform.LookAt(missileHead);
        }
    }

    //public void FindMissile()
    //{
    //    missile = GameObject.FindWithTag("Missile");
    //    modifiedOffset = missile.transform.TransformDirection(offset);
    //    missileHead = missile.transform.Find("Missile Head");
    //    Debug.Log(offset);
    //    Debug.Log(modifiedOffset);
    //}
}