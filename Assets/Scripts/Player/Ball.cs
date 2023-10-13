using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class Ball : MonoBehaviour
{
    Vector3 distanceP,distanceT;
    [SerializeField] private LineRenderer lineRenderer; // aim için line
    private bool isIdle; // top duruyor mu hareketli mi boolu
    private bool isAiming; // oyuncu aim halinde mi boolu
    [SerializeField] private float stopVelocity; // topun durmasý için min hýz
    [SerializeField] private float shotPower;
    private Rigidbody rb;
    public Camera cam;
    MoveAroundObject moveAroundObject;
    Zoom zoom;
    public bool isShooting; // shoot hali boolu
    public static bool shooted;
    public GameObject LineRenderer;
    public bool shootCloser;
    Vector3? worldPoint;
    public Vector3 mousePos, upForce;
    public float curveValue, forceValue;
    public float lineX;
    Camera cam2;
    public PhotonView view;
    private GameObject hole;
    [SerializeField] public static float timer;
    public GameObject ronaldinho,messi;
    Animator footballerAnimator,trivelaAnimator;
    GameObject OurFootballer,TrivelaFootballer;
    public static bool waitForShoot, waitForShootTri;
    public float waitForShootTimer,waitForShootTriTimer,whichAnim;
    public static bool footballerTeleport;
    public static bool gameEnder;
    Photon.Realtime.Player player;
    public bool gravityChanger;
    public static bool lineRendererOn;
    public static bool lineRendererController;
    public static bool Player1,Player2,Player3,Player4;


    private void Start()
    {
        PlayerPrefs.GetInt("FootballerChooser", 0);
        lineRendererController = false;
        lineRendererOn = false;
        gameEnder = false;
        whichAnim = 0;
        timer = 20;
        waitForShoot = false;
        waitForShootTri = false;
        footballerTeleport = false;
        waitForShootTimer = 0;
        waitForShootTriTimer = 0;
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        cam2 = GameObject.FindGameObjectWithTag("AfterCamera").GetComponent<Camera>() as Camera;
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
        hole = GameObject.FindGameObjectWithTag("Hole");
        cam.GetComponent<AudioListener>().enabled = true;
        cam2.GetComponent<AudioListener>().enabled = false;
        cam.enabled = (true);
        cam2.enabled = (false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", false } });
        if (PlayerPrefs.GetInt("FootballerChooser") == 1)
        {
            OurFootballer = Instantiate(ronaldinho, new Vector3(transform.position.x + 2.6f, transform.position.y - 0.3f, transform.position.z + 1.6f), Quaternion.identity);
            TrivelaFootballer = Instantiate(ronaldinho, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
            distanceP = transform.position - OurFootballer.transform.position;
            distanceT = transform.position - TrivelaFootballer.transform.position;
            OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            TrivelaFootballer.transform.rotation= Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            footballerAnimator = OurFootballer.GetComponent<Animator>();
            trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
        }
        else if (PlayerPrefs.GetInt("FootballerChooser") == 0)
        {
            OurFootballer = Instantiate(messi, new Vector3(transform.position.x + 2.6f, transform.position.y -0.3f, transform.position.z + 1.6f), Quaternion.identity);
            TrivelaFootballer = Instantiate(messi, new Vector3(transform.position.x + 1.5f, transform.position.y - 0.3f, transform.position.z + 1.7f), Quaternion.identity);
            distanceP = transform.position - OurFootballer.transform.position;
            distanceT = transform.position - TrivelaFootballer.transform.position;
            OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
            footballerAnimator = OurFootballer.GetComponent<Animator>();
            trivelaAnimator = TrivelaFootballer.GetComponent<Animator>();
        }

        OurFootballer.SetActive(false);
        foreach (Player player in PhotonNetwork.PlayerList)
        {

            if (player.IsMasterClient)
            {
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });

            }
            else
            {
                player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
            }
        }
    }
    private void Awake()
    {
        shootCloser = false;
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false; // baþta line görünmemesi için
        
    }

    private void Update()
    {
        TrivelaFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
        if (view.IsMine)
        {
            if (rb.velocity.magnitude < stopVelocity) // topun durmasý için hýz kontrolü
            {
                Stop();
                if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
                {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
                    {
                        timer -= Time.deltaTime;
                        if (timer > 0)
                        {
                            ProcessAim();
                        }
                        else
                        {
                            lineRendererController = false;
                            shooted = false;
                            shootCloser = true;
                            Zoom.changeFovBool = false;
                            if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
                            {
                                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                                PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                            }
                            timer = 20f;
                        }
                    }
                }
            }
            if (waitForShoot == true)
            {
                waitForShootTimer += Time.deltaTime;
                Zoom.changeFovBool = false;
            }
            if (waitForShootTri == true)
            {
                waitForShootTriTimer += Time.deltaTime;
                Zoom.changeFovBool = false;
            }
            if (waitForShootTimer >= 0.9f)
            {
                //OurFootballer.SetActive(false);
                waitForShoot = false;
                waitForShootTimer = 0;
                footballerAnimator.SetBool("penaltyKick", false);
                trivelaAnimator.SetBool("trivela", false);
                OnMouseShootPart();
            }
            if (waitForShootTriTimer >= 0.65f)
            {
                //OurFootballer.SetActive(false);
                waitForShootTri = false;
                waitForShootTriTimer = 0;
                footballerAnimator.SetBool("penaltyKick", false);
                trivelaAnimator.SetBool("trivela", false);
                OnMouseShootPart();
            }
            if (shooted == false && rb.velocity.magnitude < stopVelocity && Input.GetMouseButton(0))
            {
                if (lineRendererOn == false)
                {
                    OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
                    TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 60);
                }
                else
                {
                    OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
                    TrivelaFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis / 300);
                }
                
            }
            if (rb.velocity.magnitude < stopVelocity && footballerTeleport==false)
            {
                distanceP.y = 0.3f;
                distanceT.y = 0.3f;
                OurFootballer.transform.position = transform.position - distanceP;
                TrivelaFootballer.transform.position = transform.position - distanceT;
                OurFootballer.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                TrivelaFootballer.transform.rotation= Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y - 320, 0);
                footballerTeleport = true;
            }
            else if(rb.velocity.magnitude > stopVelocity)
            {
                footballerTeleport = false;
            }
        }
        //if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null)
        //{
        //    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"] && PhotonNetwork.CurrentRoom.PlayerCount != 1)
        //    {
        //        PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        //    }
        //}
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties["holeC"] != null)
            {
                if ((bool)player.CustomProperties["holeC"] && PhotonNetwork.CurrentRoom.PlayerCount != 1)
                {
                    player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
                    player.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
                }
            }
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
            {
                if ((bool)player.CustomProperties["turn"])
                {
                    Debug.Log("Player: " + player.ActorNumber.ToString() + "is kicking");
                    if (player.ActorNumber ==1)
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
                    else if(player.ActorNumber == 3)
                    {
                        Debug.Log("actor: " + player.ActorNumber);
                        Player1 = false;
                        Player2 = false;
                        Player3 = true;
                        Player4 = false;
                    }
                    else if (player.ActorNumber == 4)
                    {
                        Debug.Log("actor: " + player.ActorNumber);
                        Player1 = false;
                        Player2 = false;
                        Player3 = false;
                        Player4 = true;
                    }
                }

            }

        }
        //if (!view.IsMine && !(bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
        //{
        //    // Spectator mode: Do not process input for spectator
        //    return;
        //}


        if (gravityChanger)
        {
            Physics.gravity = Vector3.zero;
        }
        else
        {
            Physics.gravity = new Vector3(0,-12,0);
        }
        //Debug.Log(gravityChanger);
        //Debug.Log("gravity: " + Physics.gravity);
        
    }
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
                OurFootballer.SetActive(true);
                if (mousePos.x > Screen.width / 2)
                {
                    OurFootballer.SetActive(true);
                    TrivelaFootballer.SetActive(false);
                    footballerAnimator.SetBool("penaltyKick", true);
                    distanceP = transform.position - OurFootballer.transform.position;
                    distanceT = transform.position - TrivelaFootballer.transform.position;
                    waitForShoot = true; 
                }
                else
                {
                    TrivelaFootballer.SetActive(true);
                    OurFootballer.SetActive(false);
                    trivelaAnimator.SetBool("trivela", true);
                    distanceT = transform.position - TrivelaFootballer.transform.position;
                    distanceP = transform.position - OurFootballer.transform.position;
                    waitForShootTri = true;

                }
                //footballerAnimator.SetBool("penaltyKick", true);
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
        lineRendererController = false;
        shootCloser = true;
        Zoom.changeFovBool = false;
        ShotCounter.ShotCount += 1;
        timer = 20f;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
        {
            PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        }
        gravityChanger = false;


    }
    
    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            gravityChanger = false;
            return; // exit method
        }
        if (!shooted)
        {
            worldPoint = CastMouseClickRay();// world pointi belirlemek için clickten ray yolla 
        }

        if (!worldPoint.HasValue) // ray bi þeye çarptý mý diye check
        {
            return; // exit method
        }
        DrawLine(transform.position - (worldPoint.Value - transform.position));// aim line çiz
        if (lineRendererController == false)
        {
            lineRendererOn = true;
            lineRendererController = true;
        }
        
        //aþaðýdaki ifleri topa iyice yakýn olduðu zaman býrakabilmesi için kullanabiliriz
        if ((worldPoint.Value - transform.position).y < 0)
        {
            //Debug.Log("y kucuk");
            //cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y, (cam.transform.position.z - 2*Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z)));
        }
        else
        {
            //Debug.Log("y buyuk");
        }
        if (Input.GetMouseButtonUp(0)) // parmaðýmý çektim mi
        {
            lineRendererOn = false;
            shooted = true;
            Zoom.changeFovBool = true;
        }
        
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
            Vector3 direction = worldPoint - transform.position; // lineýn directioný
            float lineLength = direction.magnitude; // lineýn uzunluðunun hesaplanmasý
            float maxLength = 1.25f; // max line length

            if (lineLength > maxLength) // maxla current length kýyasý
            {
                direction = direction.normalized * maxLength;
                worldPoint = transform.position + direction;
            }

            Vector3[] positions = { transform.position, worldPoint };
            lineRenderer.SetPositions(positions);

            for (int i = 0; i < positions.Length; i++) // yukarýda aldýðýmýz positionlarý looplama
            {
                positions[i].y = gameObject.transform.position.y + .02f; // lineýn y axisi fixleme
            }

            lineRenderer.SetPositions(positions); // update positions
            lineRenderer.enabled = true; // line visible
            shootCloser = false;
        }
        else
        {
            lineRenderer.enabled = false; // line visible}
        }

    }
    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
            );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
            );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;

        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity)) // neardan far'a ray yolla
        {
            return hit.point; // eðer ray bi þeye çarparsa return hit point
        }
        else
        {
            return null; // eðer ray bi þeye çarpmazsa return null
        }

    }

    private void Stop()
    {
        rb.velocity = Vector3.zero; // topun velocitysini 0a eþitle
        rb.angularVelocity = Vector3.zero; // topun angular velocitysini 0a eþitle
        gravityChanger = true;
        isIdle = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {

            if (view.IsMine)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", true } });
                cam.enabled = (false);
                cam.GetComponent<Zoom>().enabled = false;
                cam.GetComponent<AudioListener>().enabled = false;
                cam2.GetComponent<AudioListener>().enabled = true;
                cam2.enabled = (true);
                CheckAllPlayers();
            }
        }
    }
    private void CheckAllPlayers()
    {
        StartCoroutine(DelayCheck(1f));
    }

    [PunRPC]
    private void NotifyConditionMet()
    {
        if (GameEnder.EndGame)
        {
            gameEnder = true;
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
        bool allPlayersReady = true;
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
            view.RPC("NotifyConditionMet", RpcTarget.All);//herkes ayný holeC bool statete
        }
    }

    
}
