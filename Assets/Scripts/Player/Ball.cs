using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    #region Definings
    Vector3 screenMousePosFar;
    Vector3 screenMousePosNear;
    Vector3 worldMousePosFar;
    Vector3 worldMousePosNear;
    Vector3 distanceP,distanceT;
    [SerializeField] private LineRenderer lineRenderer; // aim i�in line
    public static bool isIdle; // top duruyor mu hareketli mi boolu
    public static bool isAiming; // oyuncu aim halinde mi boolu
    bool OurTurn;
    [SerializeField] private float stopVelocity; // topun durmas� i�in min h�z
    [SerializeField] private float shotPower,ace;
    private Rigidbody rb;
    public Camera cam;
    MoveAroundObject moveAroundObject;
    Zoom zoom;
    public bool isShooting; // shoot hali boolu
    public static bool shooted;
    public GameObject LineRenderer;
    public static bool shootCloser;
    Vector3? worldPoint;
    public Vector3 mousePos, upForce;
    public float curveValue, forceValue;
    public float lineX;
    Camera barrierCam;
    public PhotonView view;
    private GameObject hole;
    [SerializeField] public static float timer;
    //public GameObject Ronaldinho,Messi;
    //Animator footballerAnimator,trivelaAnimator;
    //GameObject OurFootballer,TrivelaFootballer;
    //public static bool waitForShoot, waitForShootTri;
    //public float waitForShootTimer,waitForShootTriTimer,whichAnim;
    //public static bool footballerTeleport;
    public static bool gameEnder;
    Photon.Realtime.Player player;
    //public static bool lineRendererOn;
    //public static bool lineRendererController;
    public static bool Player1,Player2,Player3,Player4;
    bool OurFootballerCloser;
    public LayerMask ground;
    Ray rayNorm;
    Ray rayTri;
    bool nextPlayerTurn;
    bool shotClicked;
    bool barrier;
    public static bool challangeCheck;
    GameObject spectatorCanvas;
    bool allPlayersReady;
    #endregion


    private void Start()
    {
        #region DefiningAtStart
        challangeCheck = false;
        PlayerPrefs.GetInt("FootballerChooser", 0);
        OurFootballerCloser = false;
        OurTurn = true;
        //lineRendererController = false;
        //lineRendererOn = false;
        gameEnder = false;
        //whichAnim = 0;
        timer = 20;
        //waitForShoot = false;
        //waitForShootTri = false;
        //footballerTeleport = false;
        //waitForShootTimer = 0;
        //waitForShootTriTimer = 0;
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        barrierCam = GameObject.FindGameObjectWithTag("BarrierCam").GetComponent<Camera>() as Camera;
        spectatorCanvas = GameObject.FindGameObjectWithTag("SpectatorCanvas");
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
        hole = GameObject.FindGameObjectWithTag("Hole");
        cam.GetComponent<AudioListener>().enabled = true;
        cam.enabled = (true);
        
        //PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        

        #endregion

        #region CommentedOldAnimations
        //if (view.IsMine)
        //{

        //    if (PlayerPrefs.GetInt("FootballerChooser") == 1)
        //    {
        //        OurFootballer = PhotonNetwork.Instantiate(Ronaldinho.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
        //        TrivelaFootballer = PhotonNetwork.Instantiate(Ronaldinho.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
        //        distanceP = transform.position - OurFootballer.transform.position;
        //        distanceT = transform.position - TrivelaFootballer.transform.position;
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //        footballerAnimator = OurFootballer.GetComponent<Animator>();
        //        trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
        //        //OurFootballer.SetActive(false);
        //        view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
        //        //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
        //        view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
        //        OurFootballerCloser = true;
        //    }
        //    else if (PlayerPrefs.GetInt("FootballerChooser") == 0)
        //    {
        //        OurFootballer = PhotonNetwork.Instantiate(Messi.name, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
        //        TrivelaFootballer = PhotonNetwork.Instantiate(Messi.name, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
        //        distanceP = transform.position - OurFootballer.transform.position;
        //        distanceT = transform.position - TrivelaFootballer.transform.position;
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //        footballerAnimator = OurFootballer.GetComponent<Animator>();
        //        trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
        //        //OurFootballer.SetActive(false);
        //        view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
        //        //en ba�ta millet �ok yak�nken futbolcular �ne ge�mesin diye
        //        view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
        //        OurFootballerCloser = true;
        //    }
        //}

        #endregion



        foreach (Player player in PhotonNetwork.PlayerList)
        {

            if (player.IsMasterClient)
            {
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", false } });

            }
            else
            {
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", false } });
            }
        }
        
    }

    #region CommentedOldAnimations
    //[PunRPC]
    //void HideOurFootballer(string footballerPhotonViewId)
    //{
    //    PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
    //    if (targetFootballer != null)
    //    {
    //        targetFootballer.gameObject.SetActive(false);
    //        //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = false;
    //    }
    //}

    //[PunRPC]
    //void ShowOurFootballer(string footballerPhotonViewId)
    //{
    //    PhotonView targetFootballer = PhotonView.Find(int.Parse(footballerPhotonViewId));
    //    if (targetFootballer != null)
    //    {
    //        targetFootballer.gameObject.SetActive(true);
    //        //targetFootballer.gameObject.GetComponent<MeshRenderer>().enabled = true;
    //    }
    //}

    #endregion

    private void Awake()
    {
        shootCloser = false;
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false; // ba�ta line g�r�nmemesi i�in
        
    }

    private void Update()
    {
        if (/*PhotonNetwork.CurrentRoom.PlayerCount == 1*/CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] && !GameEnder.spectCanvasClose)
            {
                barrierCam.gameObject.SetActive(true);
                spectatorCanvas.SetActive(true);
            }
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
            {
                if ((bool)player.CustomProperties["holeC"] && (bool)player.CustomProperties["turn"] && !allPlayersReady)
                {
                    //barrierCam.gameObject.SetActive(true);
                    //spectatorCanvas.SetActive(true);
                    player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                    player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                }
            }
            
        }

        #region CommentedOldAnimations
        ////sol art� sa� eksi
        //if (OurFootballer != null)
        //{
        //    rayNorm = new Ray(OurFootballer.transform.position + Vector3.up * 10, Vector3.down); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
        //}
        //if (TrivelaFootballer != null)
        //{
        //    rayTri = new Ray(TrivelaFootballer.transform.position + Vector3.up * 10, Vector3.down); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
        //}


        //RaycastHit hit;

        //if (Physics.Raycast(rayNorm, out hit, Mathf.Infinity, ground))
        //{
        //    OurFootballer.transform.position = new Vector3(OurFootballer.transform.position.x, hit.point.y - 0.2f, OurFootballer.transform.position.z);
        //}
        //if (Physics.Raycast(rayTri, out hit, Mathf.Infinity, ground))
        //{
        //    TrivelaFootballer.transform.position = new Vector3(TrivelaFootballer.transform.position.x, hit.point.y - 0.2f, TrivelaFootballer.transform.position.z);
        //}
        //if (!Input.GetMouseButton(0))
        //{
        //    if (OurFootballer != null)
        //    {
        //        footballerAnimator.SetBool("rightWalk", false);
        //        footballerAnimator.SetBool("leftWalk", false);
        //    }

        //    if (TrivelaFootballer != null)
        //    {
        //        trivelaAnimator.SetBool("rightWalk", false);
        //        trivelaAnimator.SetBool("leftWalk", false);
        //    }
        //}
        //else
        //{
        //    if (MoveAroundObject.rotationaroundyaxis < 0)
        //    {
        //        if (OurFootballer != null)
        //        {
        //            footballerAnimator.SetBool("rightWalk", true);
        //            footballerAnimator.SetBool("leftWalk", false);
        //        }
        //        if (TrivelaFootballer != null)
        //        {
        //            trivelaAnimator.SetBool("rightWalk", true);
        //            trivelaAnimator.SetBool("leftWalk", false);
        //        }
        //    }
        //    else if (MoveAroundObject.rotationaroundyaxis > 0)
        //    {
        //        if (OurFootballer != null)
        //        {
        //            footballerAnimator.SetBool("rightWalk", false);
        //            footballerAnimator.SetBool("leftWalk", true);
        //        }
        //        if (TrivelaFootballer != null)
        //        {
        //            trivelaAnimator.SetBool("rightWalk", false);
        //            trivelaAnimator.SetBool("leftWalk", true);
        //        }
        //    }
        //}
        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "Hole1")
        //{
        //    if (TrivelaFootballer != null)
        //    {
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //    }
        //    if (OurFootballer != null)
        //    {
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        //    }
        //}
        //else if(scene.name == "Hole2" || scene.name=="Hole2Rainy")
        //{
        //    if (TrivelaFootballer != null)
        //    {
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y +30, 0);
        //    }
        //    if (OurFootballer != null)
        //    {
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y +30, 0);
        //    }
        //}
        //else if (scene.name == "Hole3" || scene.name == "Hole3Rainy")
        //{
        //    if (TrivelaFootballer != null)
        //    {
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 65, 0);
        //    }
        //    if (OurFootballer != null)
        //    {
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y + 65, 0);
        //    }
        //}
        //else if (scene.name == "Hole4" || scene.name == "Hole4Rainy")
        //{
        //    if (TrivelaFootballer != null)
        //    {
        //        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - ace, 0);
        //    }
        //    if (OurFootballer != null)
        //    {
        //        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - ace, 0);
        //    }
        //}

        #endregion

        
        
        if (view.IsMine)
            {
            if (rb.velocity.magnitude < stopVelocity) // topun durmas� i�in h�z kontrol�
            {
                Stop();
                if (shotClicked)
                {
                    GetTurn();
                }

                if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null /*&& PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null*/)
                {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"]/* && !(bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"]*/)
                    {
                        
                        if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
                        {
                            barrierCam.gameObject.SetActive(false);
                            spectatorCanvas.SetActive(false);
                        }
                        
                        timer -= Time.deltaTime;
                        
                        if (timer > 0)
                        {
                            ProcessAim();

                        }
                        else
                        {
                            AnimationFootballer.lineRendererController = false;
                            shooted = false;
                            shootCloser = true;
                            Zoom.changeFovBool = false;
                            GetTurn();
                        }

                    }
                    else
                    {
                        if (PhotonNetwork.CurrentRoom.PlayerCount != 1 && !GameEnder.spectCanvasClose)
                        {
                            barrierCam.gameObject.SetActive(true);
                            spectatorCanvas.SetActive(true);
                        }
                    }
                }

            }
            #region CommentedOldAnimations
            //if (waitForShoot == true)
            //{
            //    waitForShootTimer += Time.deltaTime;
            //    Zoom.changeFovBool = false;
            //}
            //if (waitForShootTri == true)
            //{
            //    waitForShootTriTimer += Time.deltaTime;
            //    Zoom.changeFovBool = false;
            //}
            //if (waitForShootTimer >= 0.9f)
            //{
            //    //OurFootballer.SetActive(false);
            //    waitForShoot = false;
            //    waitForShootTimer = 0;
            //    footballerAnimator.SetBool("penaltyKick", false);
            //    trivelaAnimator.SetBool("trivela", false);
            //    OnMouseShootPart();
            //}

            #endregion

            if (AnimationFootballer.AcceptShoot == true)
            {
                OnMouseShootPart();
                AnimationFootballer.AcceptShoot = false;
            }
            #region CommentedOldAnimations
            //if (waitForShootTriTimer >= 0.65f)
            //{
            //    //OurFootballer.SetActive(false);
            //    waitForShootTri = false;
            //    waitForShootTriTimer = 0;
            //    footballerAnimator.SetBool("penaltyKick", false);
            //    trivelaAnimator.SetBool("trivela", false);
            //    OnMouseShootPart();
            //}
            //if (shooted == false && rb.velocity.magnitude < stopVelocity && Input.GetMouseButton(0))
            //{
            //    if (lineRendererOn == false)
            //    {
            //        OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            //        TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
            //    }
            //    else
            //    {
            //        OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            //        TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
            //    }

            //}
            //if (rb.velocity.magnitude < stopVelocity && footballerTeleport==false)
            //{
            //    distanceP.y = 0.3f;
            //    distanceT.y = 0.3f;
            //    OurFootballer.transform.position = transform.position - distanceP;
            //    TrivelaFootballer.transform.position = transform.position - distanceT;
            //    OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            //    TrivelaFootballer.transform.rotation= Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            //    footballerTeleport = true;
            //}
            //else if(rb.velocity.magnitude > stopVelocity)
            //{
            //    footballerTeleport = false;
            //}

            #endregion
        }


        #region TurnLogic

        //if (/*PhotonNetwork.CurrentRoom.PlayerCount == 1*/CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
        //{
        //    PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //}

        //if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus)
        //{
        //    foreach (Player player in PhotonNetwork.PlayerList)
        //    {
        //        if (player.CustomProperties["holeC"] != null && player.CustomProperties["turn"] != null)
        //        {
        //            if ((bool)player.CustomProperties["holeC"] && /*PhotonNetwork.CurrentRoom.PlayerCount != 1*/(!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
        //            {
        //                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        //                player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //            }
        //        }
        //    }
        //}
        //if ((CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus))
        //{
        //    if(PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null && PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        //        {
        //        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] && (bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"] && (!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
        //        {
        //            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        //            PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //        }
        //    }
        //}
        //else if ((CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament))
        //{
        //    if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null && PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        //    {
        //        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] && (bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"] && (!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
        //        {
        //            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        //            PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //        }
        //    }
        //}
        //if ((CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament))
        //{
        //    if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null && PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        //    {
        //        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] && (bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"] && (!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
        //        {
        //            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        //            PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //        }
        //    }
        //}

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
            {
                if ((bool)player.CustomProperties["turn"]/* && !GameEnder.EndGamePanelOpen*/)
                {
                    if (CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
                    {
                        if (player.ActorNumber == 1)
                        {

                            Player1 = true;
                            Player2 = false;
                            Player3 = false;
                            Player4 = false;

                        }
                    }
                    if (CreateAndJoinRandomRooms.versus || CreateAndJoinRooms.versus)
                    {
                        if (player.ActorNumber == 1)
                        {

                            Player1 = true;
                            Player2 = false;
                            Player3 = false;
                            Player4 = false;

                        }
                        else if (player.ActorNumber == 2)
                        {
                            Player1 = false;
                            Player2 = true;
                            Player3 = false;
                            Player4 = false;
                        }
                    }
                    if (CreateAndJoinRandomRooms.Tournament || CreateAndJoinRooms.Tournament)
                    {
                        if (player.ActorNumber == 1)
                        {
                            //if (player.CustomProperties["holeC"] != null)
                            //{
                            //    if ((bool)player.CustomProperties["holeC"] && /*PhotonNetwork.CurrentRoom.PlayerCount != 1*/(!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
                            //    {
                            //        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                            //        player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                            //    }
                            //}
                            Player1 = true;
                            Player2 = false;
                            Player3 = false;
                            Player4 = false;

                        }
                        else if (player.ActorNumber == 2)
                        {
                            //if (player.CustomProperties["holeC"] != null)
                            //{
                            //    if ((bool)player.CustomProperties["holeC"] && /*PhotonNetwork.CurrentRoom.PlayerCount != 1*/(!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
                            //    {
                            //        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                            //        player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                            //    }
                            //}
                            Player1 = false;
                            Player2 = true;
                            Player3 = false;
                            Player4 = false;
                        }
                        else if (player.ActorNumber == 3)
                        {
                            //if (player.CustomProperties["holeC"] != null)
                            //{
                            //    if ((bool)player.CustomProperties["holeC"] && /*PhotonNetwork.CurrentRoom.PlayerCount != 1*/(!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
                            //    {
                            //        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                            //        player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                            //    }
                            //}
                            Player1 = false;
                            Player2 = false;
                            Player3 = true;
                            Player4 = false;
                        }
                        else if (player.ActorNumber == 4)
                        {
                            //if (player.CustomProperties["holeC"] != null)
                            //{
                            //    if ((bool)player.CustomProperties["holeC"] && /*PhotonNetwork.CurrentRoom.PlayerCount != 1*/(!CreateAndJoinRandomRooms.practice || !CreateAndJoinRooms.practice))
                            //    {
                            //        player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                            //        player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                            //    }
                            //}
                            Player1 = false;
                            Player2 = false;
                            Player3 = false;
                            Player4 = true;
                        }
                    }

                }

            }

        }

        #endregion

        if (lineRenderer.enabled == true || Zoom.changeFovBool)
        {
            Physics.gravity = Vector3.zero;
        }
        else
        {
            Physics.gravity = new Vector3(0,-12,0);
        }
    }
    private void OnMouseDown()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
            {
                
                if (isIdle)
                {
                    isAiming = true;
                }

                if (shooted == true)
                {
                    if (Input.GetMouseButtonDown(0) && shootCloser == false)
                    {
                        mousePos = Input.mousePosition;
                        #region CommentedOldAnimations
                        //if (mousePos.x > Screen.width / 2)
                        //{
                        //    view.RPC("ShowOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                        //    view.RPC("HideOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
                        //    OurFootballer.SetActive(true);
                        //    TrivelaFootballer.SetActive(false);
                        //    footballerAnimator.SetBool("penaltyKick", true);
                        //    distanceP = transform.position - OurFootballer.transform.position;
                        //    distanceT = transform.position - TrivelaFootballer.transform.position;
                        //    waitForShoot = true;
                        //}
                        //else
                        //{
                        //    view.RPC("ShowOurFootballer", RpcTarget.All, TrivelaFootballer.GetComponent<PhotonView>().ViewID.ToString());
                        //    view.RPC("HideOurFootballer", RpcTarget.All, OurFootballer.GetComponent<PhotonView>().ViewID.ToString());
                        //    TrivelaFootballer.SetActive(true);
                        //    OurFootballer.SetActive(false);
                        //    trivelaAnimator.SetBool("trivela", true);
                        //    distanceT = transform.position - TrivelaFootballer.transform.position;
                        //    distanceP = transform.position - OurFootballer.transform.position;
                        //    waitForShootTri = true;

                        //}
                        #endregion

                    }

                }
            }
        }

    }
    public void OnMouseShootPart()
    {
        if (mousePos.x > Screen.width / 2)
        {
            curveValue = (mousePos.x - Screen.width / 2) * 0.15f;
            if (mousePos.y > Screen.height / 2)
            {
                forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f;
                Shoot(worldPoint.Value, CurveDirection.LeftDown); // shoot
            }
            if (mousePos.y < Screen.height / 2)
            {
                forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f;
                Shoot(worldPoint.Value, CurveDirection.LeftUp); // shoot
            }
        }
        if (mousePos.x < Screen.width / 2)
        {
            curveValue = (Screen.width / 2 - (mousePos.x)) * 0.15f;
            if (mousePos.y > Screen.height / 2)
            {
                forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f;
                Shoot(worldPoint.Value, CurveDirection.RightDown); // shoot
            }
            if (mousePos.y < Screen.height / 2)
            {
                forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f;
                Shoot(worldPoint.Value, CurveDirection.RightUp); // shoot
            }
        }
        AnimationFootballer.lineRendererController = false;
        shootCloser = true;
        Zoom.changeFovBool = false;
        ShotCounter.ShotCount += 1;
        AudioManager.Instance.PlaySFX("Shoot");
        StartCoroutine(ShootedBool());
    }
    private void GetTurn()
    {
        nextPlayerTurn = true;
        timer = 20f;
        if (nextPlayerTurn)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
            {
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (player.CustomProperties["turn"] != null)
                    {
                        if ((bool)player.CustomProperties["turn"])
                        {
                            player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                            player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                        }
                    }
                    
                }
                shotClicked = false;
                nextPlayerTurn = false;

            }
        }
        
    }
    IEnumerator ShootedBool()
    {
        yield return new WaitForSeconds(0.25f);
        shotClicked = true;
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return; // exit method
        }

        if (!shooted)
        {
            worldPoint = CastMouseClickRay();// world pointi belirlemek i�in clickten ray yolla 
        }

        if (!worldPoint.HasValue) // ray bi �eye �arpt� m� diye check
        {
            return; // exit method
        }

        DrawLine(transform.position - (worldPoint.Value - transform.position));// aim line �iz

        if (AnimationFootballer.lineRendererController == false)
        {
            AnimationFootballer.lineRendererOn = true;
            AnimationFootballer.lineRendererController = true;
        }

        if (Input.GetMouseButtonUp(0)) // parma��m� �ektim mi
        {
            AnimationFootballer.lineRendererOn = false;
            shooted = true;
            Zoom.changeFovBool = true;
        }

        #region a�a��daki ifleri topa iyice yak�n oldu�u zaman b�rakabilmesi i�in kullanabiliriz
        //a�a��daki ifleri topa iyice yak�n oldu�u zaman b�rakabilmesi i�in kullanabiliriz
        //if ((worldPoint.Value - transform.position).y < 0)
        //{
        //    Debug.Log("y kucuk");
        //    //cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y, (cam.transform.position.z - 2*Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z)));
        //}
        //else
        //{
        //    Debug.Log("y buyuk");
        //}
        #endregion
    }

    public enum CurveDirection
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown
    }

    private void Shoot(Vector3 worldPoint, CurveDirection curveDirection)
    {
        isAiming = false;
        lineRenderer.enabled = false;
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint);
        float force = Mathf.Min(lineLength, 1f) * shotPower;
        float curveAmount = 0.5f; // Adjust this value to control the curve strength
        // Add curve to the direction vector based on the specified curve direction
        Vector3 curveVector = Vector3.zero;
        switch (curveDirection)
        {
            case CurveDirection.LeftUp:
                curveVector = Quaternion.AngleAxis(-curveValue, Vector3.up) * direction;
                upForce = new Vector3(0, forceValue, 0);
                break;
            case CurveDirection.LeftDown:
                curveVector = Quaternion.AngleAxis(-curveValue, Vector3.up) * direction;
                upForce = new Vector3(0, 0, 0);
                break;
            case CurveDirection.RightUp:
                curveVector = Quaternion.AngleAxis(curveValue, Vector3.up) * direction;
                upForce = new Vector3(0, forceValue, 0);
                break;
            case CurveDirection.RightDown:
                curveVector = Quaternion.AngleAxis(curveValue, Vector3.up) * direction;
                upForce = new Vector3(0, 0, 0);
                break;
        }
        Vector3 finalDirection = upForce + direction + curveAmount * curveVector;

        
        rb.AddForce(-finalDirection * force);
        isIdle = false;
        shooted = false;
    }
                   
    private void DrawLine(Vector3 worldPoint)
    {
        if (!shooted)
        {
            Vector3 direction = worldPoint - transform.position; // line�n direction�
            float lineLength = direction.magnitude; // line�n uzunlu�unun hesaplanmas�
            float maxLength = 1.25f; // max line length
            
            if (lineLength > maxLength) // maxla current length k�yas�
            {
                direction = direction.normalized * maxLength;
                worldPoint = transform.position + direction;
            }
            Vector3[] positions = { transform.position, worldPoint };
            lineRenderer.SetPositions(positions);

            for (int i = 0; i < positions.Length; i++) // yukar�da ald���m�z positionlar� looplama
            {
                positions[i].y = gameObject.transform.position.y + .02f; // line�n y axisi fixleme
            }

            lineRenderer.SetPositions(positions); // update positions
            lineRenderer.enabled = true; // line visible
            shootCloser = false;
        }
        else
        {
            lineRenderer.enabled = false; // line visible
        }

    }
    private Vector3? CastMouseClickRay()
    {
        screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
            );
        screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
            );
        if (!(bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
        {
            screenMousePosFar = Vector3.zero;
            screenMousePosNear = Vector3.zero;
        }
        else
        {
            worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
            worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        }

        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
        {
            if (OurTurn)
            {
                OurTurn = false;
                return null;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity)) // neardan far'a ray yolla
                {

                    return hit.point; // e�er ray bi �eye �arparsa return hit point

                }
                else
                {
                    return null; // e�er ray bi �eye �arpmazsa return null
                }
            }
            
        }
        else
        {
            OurTurn = true;
            return null;

        }

    }
    private void Stop()
    {
        rb.velocity = Vector3.zero; // topun velocitysini 0a e�itle
        rb.angularVelocity = Vector3.zero; // topun angular velocitysini 0a e�itle
        isIdle = true;
        
    }
    #region Bir Sonraki Hole'e Gecmesi Icin
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {

            if (view.IsMine)
            {
                if (Challenges.isChallenge)
                {
                    Debug.Log("burası challange");
                    challangeCheck = true;
                }
                else
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", true } });
                    //cam.enabled = (false);
                    //cam.GetComponent<Zoom>().enabled = false;
                    //cam.GetComponent<AudioListener>().enabled = false;
                    //cam2.GetComponent<AudioListener>().enabled = true;
                    //cam2.enabled = (true);
                    CheckAllPlayers();
                }
                
            }
        }
    }
    private void CheckAllPlayers()
    {
        StartCoroutine(DelayCheck(0.1f));
    }

    [PunRPC]
    private void NotifyConditionMet()
    {
        if (/*GameEnder.EndGame*/ SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 11 || SceneManager.GetActiveScene().buildIndex == 13)
        {
            gameEnder = true;
        }
        else
        {
            gameEnder = false;
        }
        StartCoroutine(LoadNextSceneWithDelay(1f));
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //PhotonNetwork.Destroy(gameObject);
        if (!gameEnder)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        
    }
    private IEnumerator DelayCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        allPlayersReady = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!(bool)player.CustomProperties["holeC"])// holeC false mu check
            {
                allPlayersReady = false;
                break;
            }
        }
        if (allPlayersReady)
        {
            view.RPC("NotifyConditionMet", RpcTarget.All);//herkes ayn� holeC bool statete
            //cam.GetComponent<MoveAroundObject>().enabled = false;
        }
        Debug.Log(allPlayersReady);
    }
    #endregion

}
