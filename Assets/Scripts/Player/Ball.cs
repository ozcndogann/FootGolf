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
    [SerializeField] private LineRenderer lineRenderer; // aim i�in line
    public static bool isIdle; // top duruyor mu hareketli mi boolu
    public static bool isAiming; // oyuncu aim halinde mi boolu
    bool OurTurn;
    [SerializeField] private float stopVelocity; // topun durmas� i�in min h�z
    [SerializeField] private float shotPower,ace;
    private Rigidbody rb;
    public Camera cam;
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
    public static bool gameEnder;
    Photon.Realtime.Player player;
    public static bool Player1,Player2,Player3,Player4;
    bool OurFootballerCloser;
    public LayerMask ground;
    bool nextPlayerTurn;
    bool shotClicked;
    public bool shootedNow;
    public static bool challangeCheck;
    GameObject spectatorCanvas;
    bool allPlayersReady;
    private bool spectCanvasClose;
    #endregion


    private void Start()
    {
        #region DefiningAtStart
        challangeCheck = false;
        PlayerPrefs.GetInt("FootballerChooser", 0);
        OurFootballerCloser = false;
        OurTurn = true;
        gameEnder = false;
        timer = 20;
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        barrierCam = GameObject.FindGameObjectWithTag("BarrierCam").GetComponent<Camera>() as Camera;
        spectatorCanvas = GameObject.FindGameObjectWithTag("SpectatorCanvas");
        hole = GameObject.FindGameObjectWithTag("Hole");
        cam.GetComponent<AudioListener>().enabled = true;
        cam.enabled = (true);
        spectCanvasClose = false;
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
    private void Awake()
    {
        shootCloser = false;
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false; // ba�ta line g�r�nmemesi i�in
        
    }

    private void Update()
    {
        
        if (CreateAndJoinRandomRooms.practice || CreateAndJoinRooms.practice)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] /*&& spectCanvasClose*/)
            {
                barrierCam.gameObject.SetActive(true);
                spectatorCanvas.SetActive(true);
            }
        }
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
        //    {
        //        if ((bool)player.CustomProperties["holeC"] && (bool)player.CustomProperties["turn"] && !allPlayersReady)
        //        {
        //            //barrierCam.gameObject.SetActive(true);
        //            //spectatorCanvas.SetActive(true);
        //            player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        //            player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //        }
        //    }
            
        //}

        if (view.IsMine)
            {
            if (rb.velocity.magnitude < stopVelocity) // topun durmas� i�in h�z kontrol�
            {
                Stop();
                if (shotClicked)
                {
                    GetTurn();
                }

                if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
                {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
                    {
                        if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
                        {
                            timer -= Time.deltaTime;
                            barrierCam.gameObject.SetActive(false);
                            spectatorCanvas.SetActive(false);
                            if ((bool)player.CustomProperties["holeC"] && (bool)player.CustomProperties["turn"] && !allPlayersReady)
                            {
                                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                                PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });

                            }
                        }
                        
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
                        if (PhotonNetwork.CurrentRoom.PlayerCount != 1/* && spectCanvasClose*/)
                        {
                            barrierCam.gameObject.SetActive(true);
                            spectatorCanvas.SetActive(true);
                        }
                    }
                }

            }
            

            if (AnimationFootballer.AcceptShoot == true)
            {
                OnMouseShootPart();
                AnimationFootballer.AcceptShoot = false;
            }
        }


        #region TurnLogicUI
        //commentleri sildim txt'de duruyor
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
            {
                if ((bool)player.CustomProperties["turn"])
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
                        else if (player.ActorNumber == 3)
                        {
                            Player1 = false;
                            Player2 = false;
                            Player3 = true;
                            Player4 = false;
                        }
                        else if (player.ActorNumber == 4)
                        {
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
    #region OnMouse Parts
    private void OnMouseDown()
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
                shootedNow = true;
            }
        }
    }
    public void OnMouseShootPart()
    {
        if (mousePos.x > Screen.width / 2)
        {
            curveValue = (mousePos.x - Screen.width / 2) * 0.15f / CursorController.cursorValue;
            if (mousePos.y > Screen.height / 2)
            {
                forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f / CursorController.cursorValue;
                Shoot(worldPoint.Value, CurveDirection.LeftDown); // shoot
            }
            if (mousePos.y < Screen.height / 2)
            {
                forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f / CursorController.cursorValue;
                Shoot(worldPoint.Value, CurveDirection.LeftUp); // shoot
            }
        }
        if (mousePos.x < Screen.width / 2)
        {
            curveValue = (Screen.width / 2 - (mousePos.x)) * 0.15f / CursorController.cursorValue;
            if (mousePos.y > Screen.height / 2)
            {
                forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f / CursorController.cursorValue;
                Shoot(worldPoint.Value, CurveDirection.RightDown); // shoot
            }
            if (mousePos.y < Screen.height / 2)
            {
                forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f / CursorController.cursorValue;
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
    #endregion

    #region Turn System
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
                shootedNow = false;
                nextPlayerTurn = false;

            }
        }
        
    }
    IEnumerator ShootedBool()
    {
        yield return new WaitForSeconds(0.25f);
        shotClicked = true;
    }

    #endregion

    #region ShootSystemBackGround
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

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                shootedNow = false;
            }
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
    #endregion

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
        if (SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 11 || SceneManager.GetActiveScene().buildIndex == 13)
        {
            gameEnder = true;
            spectCanvasClose = true;
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
        }
        Debug.Log(allPlayersReady);
    }
    #endregion
    
}
