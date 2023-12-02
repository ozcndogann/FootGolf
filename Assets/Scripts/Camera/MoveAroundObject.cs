using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
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
    public static bool deneme;


    //PhotonView vievv;
    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Ball").transform;
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
       
        if (Ball.shooted == false && AnimationFootballer.lineRendererOn == false)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.ContainsKey("IsTurn") && (bool)player.CustomProperties["IsTurn"])
                {
                    GameObject playerGameObject = FindPlayerGameObject(player);
                    if (playerGameObject != null)
                    {
                        target = playerGameObject.transform;
                    }
                }
            }

            cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            if (AnimationFootballer.footballerTeleport == true)
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
                    if (AnimationFootballer.footballerTeleport == true)
                    {
                        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationaroundyaxis / 60, Space.World);
                        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
                    }

                    //previousPosition = newposition;
                }
            }
        }
        else if (AnimationFootballer.lineRendererOn == true)
        {
            cam.transform.position = new Vector3(target.position.x, 1 + target.position.y, target.transform.position.z);
            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
            if (AnimationFootballer.footballerTeleport == true)
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
                    if (AnimationFootballer.footballerTeleport == true)
                    {
                        cam.transform.Rotate(new Vector3(0, .65f, 0), rotationaroundyaxis / 300, Space.World);
                        cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));
                    }

                    //previousPosition = newposition;
                }
            }
        }
        else if (AnimationFootballer.waitForShoot == true || AnimationFootballer.waitForShootTri == true)
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
    private GameObject FindPlayerGameObject(Player player)
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        foreach (PhotonView view in photonViews)
        {
            if (view.Owner == player)
            {
                return view.gameObject;
            }
        }
        return null;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        float distance = Vector3.Distance(cam.transform.position, target.transform.position);
        //Debug.DrawRay(target.transform.position, cam.transform.position, Color.green);
        if (Physics.SphereCast(cam.transform.position,0.3f, cam.transform.forward, out hit, distance))
        {
            //Gizmos.color = Color.green;
            //Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
            //Gizmos.DrawWireSphere(sphereCastMidpoint, 4);
            //Gizmos.DrawSphere(hit.point, 0.1f);
            //Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
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
                if (passHit[i].transform.gameObject.tag == "Undeletable")
                {
                    //view.RPC("ShowOurFootballer", RpcTarget.All, passHit[i].transform.gameObject.GetComponent<PhotonView>().ViewID.ToString());
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
    void OnDrawGizmos()
    {
        Vector3 origin = cam.transform.position; // Iþýnýn baþladýðý konum
        Vector3 direction = cam.transform.forward; // Iþýnýn yönü
        float radius = 0.3f; // Iþýnýn yarýçapý (ayarlayýn)
        float maxDistance = Vector3.Distance(cam.transform.position, target.transform.position);// Iþýnýn maksimum menzili (ayarlayýn)

        // Gizmos renk ayarý
        Gizmos.color = Color.blue;

        // SphereCast yörüngesini çiz
        Gizmos.DrawWireSphere(origin + direction * maxDistance, radius);
    }

    [PunRPC]
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
    //private void OnMouseDown()
    //{
    //    deneme = false;
    //}
}
