using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MoveAroundObject : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    //[SerializeField] private GameObject targetObj;
    [SerializeField] private float distanceToTarget = 4;
    private Vector3 previousPosition;
    public float heightWhileShooting;
    public static float rotationaroundyaxis;
    List<GameObject> passHit = new List<GameObject>();
    Ball ball;
    Ball1 ball1;
    public PhotonView view;
    //PhotonView vievv;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
        view = target.GetComponent<PhotonView>();
        //targetObj = GameObject.FindGameObjectWithTag("Ball");
        ball = target.GetComponent<Ball>();
        ball1 = target.GetComponent<Ball1>();
        //vievv = targetObj.GetComponent<PhotonView>();
    }
    private void Update()
    {
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    if (player.CustomProperties["turn"] != null)
        //    {
        //        if ((bool)player.CustomProperties["turn"]/* && !player.IsLocal*/)
        //        {
        //            //PhotonView.Find(player.ActorNumber);
        //            vievv = PhotonNetwork.GetPhotonView(player.ActorNumber);
        //            Debug.Log(vievv);
        //            GameObject otherPlayerObject = vievv.gameObject;
        //            target = otherPlayerObject.transform;
        //        }
        //    }
        //}



        //GameObject[] players = GameObject.FindGameObjectsWithTag("Ball");
        //foreach (GameObject p in players)
        //{

        //    if (p.GetComponent<Ball>().camlock)
        //    {
        //        target = p.transform;
        //        //Debug.Log("lala");
        //    }
        //    //else
        //    //{
        //    //    target = GameObject.FindGameObjectWithTag("Ball").transform;
        //    //}
        //}
        #region CamFollow

        if (Ball.shooted == false && Ball.lineRendererOn == false)
        {
            cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            if (Ball.footballerTeleport == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    Vector3 newposition = cam.ScreenToViewportPoint(Input.mousePosition);
                    //debug.log(newposition+"new");
                    //debug.log(previousposition+"previous");
                    Vector3 direction = previousPosition - newposition;
                    rotationaroundyaxis = direction.x * 180;

                    cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
                    //if(rotationaroundyaxis)
                    if (Ball.footballerTeleport == true)
                    {
                        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationaroundyaxis / 60, Space.World);
                        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
                    }

                    //previousPosition = newposition;
                }
            }
        }
        else if (Ball.lineRendererOn == true)
        {
            cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            if (Ball.footballerTeleport == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    Vector3 newposition = cam.ScreenToViewportPoint(Input.mousePosition);
                    //debug.log(newposition+"new");
                    //debug.log(previousposition+"previous");
                    Vector3 direction = previousPosition - newposition;
                    rotationaroundyaxis = direction.x * 180;

                    cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
                    //if(rotationaroundyaxis)
                    if (Ball.footballerTeleport == true)
                    {
                        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationaroundyaxis / 300, Space.World);
                        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
                    }

                    //previousPosition = newposition;
                }
            }
        }
        else if (Ball.waitForShoot == true || Ball.waitForShootTri == true)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, /*heightwhileshooting*/target.transform.position.y + 2, cam.transform.position.z);
        }
        else
        {
            cam.transform.position = new Vector3(cam.transform.position.x, /*heightwhileshooting*/target.transform.position.y + .397f, cam.transform.position.z);
        }
        #endregion

        #region CamFollow1
        //if (Ball1.shooted == false)
        //{
        //    cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
        //    cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //    }
        //    else if (Input.GetMouseButton(0))
        //    {
        //        Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        //        //Debug.Log(newPosition+"new");
        //        //Debug.Log(previousPosition+"previous");
        //        Vector3 direction = previousPosition - newPosition;
        //        float rotationAroundYAxis = direction.x * 180;

        //        cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
        //        //if(rotationAroundYAxis)
        //        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationAroundYAxis / 300, Space.World);
        //        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

        //        //previousPosition = newPosition;
        //    }
        //}
        //else
        //{
        //    cam.transform.position = new Vector3(cam.transform.position.x, /*heightWhileShooting*/target.transform.position.y + .397f, cam.transform.position.z);
        //}
        #endregion
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);
        if (Physics.Raycast(cam.transform.position, target.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            //Debug.Log("Did Hit");
            if (hit.transform.gameObject.tag != "Ground" && hit.transform.gameObject.tag != "Ball" && hit.transform.gameObject.tag != "Hole" /*&& hit.transform.gameObject.tag != "Undeletable"*/)
            {
                passHit.Add(hit.transform.gameObject);
                if (hit.transform.gameObject.tag == "Undeletable")
                {
                    view.RPC("HideOurFootballer", RpcTarget.All, hit.transform.gameObject.GetComponent<PhotonView>().ViewID.ToString());
                }
                else
                {
                    hit.transform.gameObject.SetActive(false);
                }
            }

        }
        if (/*Ball.shooted == true || */target.gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            for (int i = 0; i < passHit.Count; i++)
            {
                if (hit.transform.gameObject.tag == "Undeletable")
                {
                    view.RPC("ShowOurFootballer", RpcTarget.All, hit.transform.gameObject.GetComponent<PhotonView>().ViewID.ToString());
                }
                else
                {
                    passHit[i].SetActive(true);
                }
            }
        }
        else
        {
            
            //Debug.Log("Did not Hit");
            
        }

    }
    void HideOurFootballer(string footballerPhotonViewId)
    {
        PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
        if (targetFootballer != null)
        {
            targetFootballer.gameObject.SetActive(false);
            //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [PunRPC]
    void ShowOurFootballer(string footballerPhotonViewId)
    {
        PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
        if (targetFootballer != null)
        {
            targetFootballer.gameObject.SetActive(true);
            //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

}
