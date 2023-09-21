using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class Ball : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer; // aim i�in line
    private bool isIdle; // top duruyor mu hareketli mi boolu
    private bool isAiming; // oyuncu aim halinde mi boolu
    [SerializeField] private float stopVelocity; // topun durmas� i�in min h�z
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
    PunTurnManager punTurnManager;
    private GameObject hole;
    [SerializeField] public float timer;
    public GameObject footballer;
    Animator footballerAnimator;
    GameObject OurFootballer;
    public static bool waitForShoot;
    public float waitForShootTimer;
    public bool footballerTeleport;
    //public static bool holeC;
    Player player;
    private void Start()
    {
        waitForShoot = false;
        footballerTeleport = false;
        waitForShootTimer = 0;
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        cam2 = GameObject.FindGameObjectWithTag("AfterCamera").GetComponent<Camera>() as Camera;
        hole = GameObject.FindGameObjectWithTag("Hole");
        cam.GetComponent<AudioListener>().enabled = true;
        cam2.GetComponent<AudioListener>().enabled = false;
        cam.enabled = (true);
        cam2.enabled = (false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", false } });
        punTurnManager = gameObject.GetComponent<PunTurnManager>();
        OurFootballer=Instantiate(footballer, new Vector3(transform.position.x + 2.6f, transform.position.y-0.15f, transform.position.z + 1.6f),Quaternion.identity);
        OurFootballer.transform.rotation = Quaternion.Euler(0,transform.rotation.y-140, 0);
        footballerAnimator = OurFootballer.GetComponent<Animator>();
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
        lineRenderer.enabled = false; // ba�ta line g�r�nmemesi i�in
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
    }

    private void Update()
    {
        
        if (view.IsMine)
        {
            if (rb.velocity.magnitude < stopVelocity) // topun durmas� i�in h�z kontrol�
            {
                Stop();
                //ProcessAim();
                if (PhotonNetwork.LocalPlayer.CustomProperties["turn"] != null)
                {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["turn"])
                    {
                        //Stop();
                        //ProcessAim();
                        timer -= Time.deltaTime;
                        if (timer > 0)
                        {
                            ProcessAim();
                        }
                        else
                        {
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
            if (waitForShootTimer >= 0.9f)
            {
                waitForShoot = false;
                waitForShootTimer = 0;
                footballerAnimator.SetBool("penaltyKick", false);
                OnMouseShootPart();
            }
            if (shooted == false && rb.velocity.magnitude < stopVelocity && Input.GetMouseButton(0))
            {
                OurFootballer.transform.RotateAround(transform.position, Vector3.up, MoveAroundObject.rotationaroundyaxis/300);
            }
            if (rb.velocity.magnitude < stopVelocity && footballerTeleport==false)
            {
                OurFootballer.transform.position = new Vector3(transform.position.x + 2.6f, transform.position.y - 0.15f, transform.position.z + 1.6f);
                OurFootballer.transform.rotation = Quaternion.Euler(0, transform.rotation.y - 140, 0);
                footballerTeleport = true;
            }
            else if(rb.velocity.magnitude > stopVelocity)
            {
                footballerTeleport = false;
            }
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties["holeC"] != null)
        {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["holeC"])
            {
                PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
            }
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
        }
        //Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["turn"]);
        Debug.Log(timer);
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
                footballerAnimator.SetBool("penaltyKick", true);
                waitForShoot = true;
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

        shootCloser = true;
        Zoom.changeFovBool = false;
        timer = 20f;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", false } });
        PhotonNetwork.LocalPlayer.GetNext().SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "turn", true } });
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
        DrawLine(transform.position - (worldPoint.Value - transform.position)); // aim line �iz
        //a�a��daki ifleri topa iyice yak�n oldu�u zaman b�rakabilmesi i�in kullanabiliriz
        if ((worldPoint.Value - transform.position).y < 0)
        {
            //Debug.Log("y kucuk");
            //cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y, (cam.transform.position.z - 2*Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z)));
        }
        else
        {
            //Debug.Log("y buyuk");
        }
        if (Input.GetMouseButtonUp(0)) // parma��m� �ektim mi
        {
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
            return hit.point; // e�er ray bi �eye �arparsa return hit point
        }
        else
        {
            return null; // e�er ray bi �eye �arpmazsa return null
        }

    }

    private void Stop()
    {
        rb.velocity = Vector3.zero; // topun velocitysini 0a e�itle
        rb.angularVelocity = Vector3.zero; // topun angular velocitysini 0a e�itle
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
                Debug.Log("girdi");
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
        StartCoroutine(LoadNextSceneWithDelay(1f));
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //PhotonNetwork.Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            view.RPC("NotifyConditionMet", RpcTarget.All);//herkes ayn� holeC bool statete
        }
    }

    
}
