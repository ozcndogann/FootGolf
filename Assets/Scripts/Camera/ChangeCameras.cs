using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class ChangeCameras : MonoBehaviour
{
    public PhotonView view,cam1W,cam2W,cam3W,cam4W,cam5W;
    public List<Camera> Cameras;
    public GameObject CasualCam;
    GameObject cam1, cam2, cam3, cam4, cam5;
    [SerializeField] private Camera MainCam;
    Vector3 disCam1, disCam2, disCam3, disCam4, disCam5;
    int random;
    public bool randomerBool,cameraChanger;
    private Rigidbody rb;
    [SerializeField] private GameObject target;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball");
        ball = target.GetComponent<Ball>();
        cam1 = PhotonNetwork.Instantiate(CasualCam.name, new Vector3(target.transform.position.x - 4, target.transform.position.y + 1.4f, target.transform.position.z), Quaternion.identity);
        //cam1W = cam1.GetComponent<PhotonView>();
        disCam1 = target.transform.position - cam1.transform.position;
        cam2 = PhotonNetwork.Instantiate(CasualCam.name, new Vector3(target.transform.position.x + 4, target.transform.position.y + 1.4f, target.transform.position.z), Quaternion.identity);
        //cam2W = cam2.GetComponent<PhotonView>();
        disCam2 = target.transform.position - cam2.transform.position;
        cam3 = PhotonNetwork.Instantiate(CasualCam.name, new Vector3(target.transform.position.x, target.transform.position.y + 1.4f, target.transform.position.z - 4), Quaternion.identity);
        //cam3W = cam3.GetComponent<PhotonView>();
        disCam3 = target.transform.position - cam3.transform.position;
        cam4 = PhotonNetwork.Instantiate(CasualCam.name, new Vector3(target.transform.position.x, target.transform.position.y + 1.4f, target.transform.position.z + 4), Quaternion.identity);
        //cam4W = cam4.GetComponent<PhotonView>();
        disCam4 = target.transform.position - cam4.transform.position;
        cam5 = PhotonNetwork.Instantiate(CasualCam.name, new Vector3(target.transform.position.x + 4, target.transform.position.y + 1.4f, target.transform.position.z - 4), Quaternion.identity);
        //cam5W = cam5.GetComponent<PhotonView>();
        disCam5 = target.transform.position - cam5.transform.position;
        Cameras.Add(cam1.GetComponent<Camera>());
        Cameras.Add(cam2.GetComponent<Camera>());
        Cameras.Add(cam3.GetComponent<Camera>());
        Cameras.Add(cam4.GetComponent<Camera>());
        Cameras.Add(cam5.GetComponent<Camera>());
        randomerBool = false;
        cameraChanger = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MainCam.transform.rotation.eulerAngles.y);
        //cam1.transform.position = new Vector3(transform.position.x - 4, transform.position.y + 1.4f, transform.position.z);// -90 main cameradan
        cam1.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.eulerAngles.y - 90, 0);
        //cam2.transform.position = new Vector3(transform.position.x + 4, transform.position.y + 1.4f, transform.position.z);// -270 main cameradan
        cam2.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.eulerAngles.y - 270, 0);
        //cam3.transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z - 4);// -180 main cameradan
        cam3.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.eulerAngles.y - 180, 0);
        //cam4.transform.position = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z + 4);// main camerayla eþit
        cam4.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.eulerAngles.y, 0);
        //cam5.transform.position = new Vector3(transform.position.x + 4, transform.position.y + 1.4f, transform.position.z - 4); // -225 main cameradan
        cam5.transform.rotation = Quaternion.Euler(0, MainCam.transform.rotation.eulerAngles.y - 225, 0);
        if (AnimationFootballer.lineRendererOn == false && Input.GetMouseButton(0) && rb.velocity.magnitude < 0.75f && Ball.shooted==false)
        {
            cam1.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            cam2.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            cam3.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            cam4.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            cam5.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
        }
        else if(AnimationFootballer.lineRendererOn == true && Input.GetMouseButton(0) && rb.velocity.magnitude < 0.75f && Ball.shooted==false)
        {
            cam1.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            cam2.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            cam3.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            cam4.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            cam5.transform.RotateAround(target.transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
        }
        if (AnimationFootballer.waitForShoot || AnimationFootballer.waitForShootTri)
        {
            disCam1 = target.transform.position - cam1.transform.position;
            disCam2 = target.transform.position - cam2.transform.position;
            disCam3 = target.transform.position - cam3.transform.position;
            disCam4 = target.transform.position - cam4.transform.position;
            disCam5 = target.transform.position - cam5.transform.position;
            if (!randomerBool)
            {
                random = Random.Range(0, 5);
                randomerBool = true;
            }
                switch (random)
                {
                    case 0:
                    //if (cam1W.IsMine)
                    //{
                    cam1.GetComponent<Camera>().enabled = true;
                    MainCam.GetComponent<Camera>().enabled = false;
                    //}  
                    break;
                    case 1:
                    //if (cam2W.IsMine)
                    //{
                    cam2.GetComponent<Camera>().enabled = true;
                    MainCam.GetComponent<Camera>().enabled = false;
                    //}   
                    break;
                    case 2:
                    //if (cam3W.IsMine)
                    //{
                    cam3.GetComponent<Camera>().enabled = true;
                    MainCam.GetComponent<Camera>().enabled = false;
                    //}
                    break;
                    case 3:
                    //if (cam4W.IsMine)
                    //{
                    cam4.GetComponent<Camera>().enabled = true;
                    MainCam.GetComponent<Camera>().enabled = false;
                    //}
                    break;
                    case 4:
                    //if (cam5W.IsMine)
                    //{
                    cam5.GetComponent<Camera>().enabled = true;
                    MainCam.GetComponent<Camera>().enabled = false;
                    //}
                    break;
                }
        }
        else
        {
                randomerBool = false;
                MainCam.GetComponent<Camera>().enabled = true;
                cam1.GetComponent<Camera>().enabled = false;
                cam2.GetComponent<Camera>().enabled = false;
                cam3.GetComponent<Camera>().enabled = false;
                cam4.GetComponent<Camera>().enabled = false;
                cam5.GetComponent<Camera>().enabled = false;
        }
        if (rb.velocity.magnitude < 0.75f && cameraChanger == false)
        {
            cam1.transform.position = target.transform.position - disCam1;
            cam2.transform.position = target.transform.position - disCam2;
            cam3.transform.position = target.transform.position - disCam3;
            cam4.transform.position = target.transform.position - disCam4;
            cam5.transform.position = target.transform.position - disCam5;
            cameraChanger = true;
        }
        else if (rb.velocity.magnitude > 0.75f)
        {
            cameraChanger = false;
        }
    }
}
