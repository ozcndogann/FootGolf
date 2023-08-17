using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class Ball : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private LineRenderer lineRenderer;
    private bool isIdle;
    private bool isAiming;
    [SerializeField] private float stopVelocity;
    [SerializeField] private float shotPower;
    private Rigidbody rb;
    public Camera cam;
    MoveAroundObject moveAroundObject;
    Zoom zoom;
    public bool isShooting;
    public static bool shooted;
    public GameObject LineRenderer;
    public bool shootCloser;
    Vector3? worldPoint;
    public Vector3 mousePos, upForce;
    public float curveValue, forceValue;
    public float lineX;
    Camera cam2;
    public PhotonView view;
    private bool isMyTurn = false;
    private bool shotTaken = false;
    Player player;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
        cam2 = GameObject.FindGameObjectWithTag("AfterCamera").GetComponent<Camera>() as Camera;
        cam.GetComponent<AudioListener>().enabled = true;
        cam2.GetComponent<AudioListener>().enabled = false;
        cam.enabled = true;
        cam2.enabled = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "holeC", false } });

        if (photonView.IsMine)
        {
            isMyTurn = true;
            shotTaken = false;
        }
    }

    private void Awake()
    {
        shootCloser = false;
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isMyTurn && !shotTaken)
        {
            if (rb.velocity.magnitude < stopVelocity)
            {
                Stop();
                ProcessAim();
            }
        }

        lineX = lineRenderer.GetPosition(1).x;
    }

    private void OnMouseDown()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isIdle)
        {
            isAiming = true;
        }

        if (shooted == true && !shotTaken)
        {
            if (Input.GetMouseButtonDown(0) && shootCloser == false)
            {
                mousePos = Input.mousePosition;
                if (mousePos.x > Screen.width / 2)
                {
                    curveValue = (mousePos.x - Screen.width / 2) * 0.15f;
                    if (mousePos.y > Screen.height / 2)
                    {
                        forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f;
                        Shoot(worldPoint.Value, CurveDirection.LeftDown);
                    }
                    if (mousePos.y < Screen.height / 2)
                    {
                        forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f;
                        Shoot(worldPoint.Value, CurveDirection.LeftUp);
                    }
                }
                if (mousePos.x < Screen.width / 2)
                {
                    curveValue = (Screen.width / 2 - (mousePos.x)) * 0.15f;
                    if (mousePos.y > Screen.height / 2)
                    {
                        forceValue = (mousePos.y + 300 - Screen.height / 2) * 0.0015f;
                        Shoot(worldPoint.Value, CurveDirection.RightDown);
                    }
                    if (mousePos.y < Screen.height / 2)
                    {
                        forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.002f;
                        Shoot(worldPoint.Value, CurveDirection.RightUp);
                    }
                }

                shootCloser = true;
                Zoom.changeFovBool = false;
            }

        }
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }
        if (!shooted)
        {
            worldPoint = CastMouseClickRay();
        }

        if (!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(transform.position - (worldPoint.Value - transform.position));

        if ((worldPoint.Value - transform.position).y < 0)
        {
            // ...
        }
        else
        {
            // ...
        }
        if (Input.GetMouseButtonUp(0))
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
            Vector3 direction = worldPoint - transform.position;
            float lineLength = direction.magnitude;
            float maxLength = 1.25f;

            if (lineLength > maxLength)
            {
                direction = direction.normalized * maxLength;
                worldPoint = transform.position + direction;
            }

            Vector3[] positions = { transform.position, worldPoint };
            lineRenderer.SetPositions(positions);

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].y = gameObject.transform.position.y + 0.02f;
            }

            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
            shootCloser = false;
        }
        else
        {
            lineRenderer.enabled = false;
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
            view.RPC("NotifyConditionMet", RpcTarget.All);//herkes ayný holeC bool statete
        }
    }

    // Other methods and MonoBehaviourPunCallbacks implementations...

    [PunRPC]
    private void TakeShotRPC()
    {
        // Shooting logic...
        shotTaken = true;
        isMyTurn = false;
        photonView.RPC("EndTurnRPC", RpcTarget.All);
    }

    [PunRPC]
    private void EndTurnRPC()
    {
        
        // Turn ending logic...
        if (photonView.IsMine)
        {
            shotTaken = false;
            // Calculate and set next active player's photonView.ViewID...
            photonView.RPC("StartTurnRPC", RpcTarget.All, player.GetNext());
        }
    }

    [PunRPC]
    private void StartTurnRPC(int nextPlayerViewID)
    {
        isMyTurn = photonView.ViewID == nextPlayerViewID;
    }

    public void StartTurn()
    {
        photonView.RPC("StartTurnRPC", RpcTarget.All, photonView.ViewID);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Implement synchronization of variables...
    }
}


