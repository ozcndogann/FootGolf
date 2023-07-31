using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Ball : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer; // aim için line
    private bool isIdle; // top duruyor mu hareketli mi boolu
    private bool isAiming; // oyuncu aim halinde mi boolu
    [SerializeField] private float stopVelocity; // topun durmasý için min hýz
    [SerializeField] private float shotPower; 
    private Rigidbody rb; 
    public GameObject cam;
    MoveAroundObject moveAroundObject;
    Zoom zoom;
    public bool isShooting; // shoot hali boolu
    public static bool shooted;
    public GameObject LineRenderer;
    public bool shootCloser;
    Vector3? worldPoint;
    public Vector3 mousePos,upForce;
    public float curveValue,forceValue;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Awake()
    {
        shootCloser=false;
        rb = GetComponent<Rigidbody>(); 
        isAiming = false; 
        lineRenderer.enabled = false; // baþta line görünmemesi için
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
        //Debug.Log(Screen.width / 2);
        //Debug.Log(Screen.height / 2);
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (rb.velocity.magnitude < stopVelocity) // topun durmasý için hýz kontrolü
            {
                Stop();
                ProcessAim();
            }
        }
        
    }

    private void OnMouseDown()
    {
        if (view.IsMine)
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
                            forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.00162f;
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
                            forceValue = (Screen.height / 2 + (300) - (mousePos.y)) * 0.00162f;
                            Shoot(worldPoint.Value, CurveDirection.RightUp); // shoot
                        }
                    }

                    shootCloser = true;
                    Zoom.changeFovBool = false;
                }

            }
        }
        
    }
    
    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
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
        DrawLine(transform.position - (worldPoint.Value - transform.position)); // aim line çiz
        //aþaðýdaki ifleri topa iyice yakýn olduðu zaman býrakabilmesi için kullanabiliriz
        if ((worldPoint.Value - transform.position).y < 0)
        {
            Debug.Log("y kucuk");
            //cam.transform.position = new Vector3(cam.transform.position.x,cam.transform.position.y, (cam.transform.position.z - 2*Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z)));
        }
        else
        {
            Debug.Log("y buyuk");
        }
        if (Input.GetMouseButtonUp(0)) // parmaðýmý çektim mi
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
            Vector3 direction = worldPoint - transform.position; // lineýn directioný
            float lineLength = direction.magnitude; // lineýn uzunluðunun hesaplanmasý
            float maxLength = 1f; // max line length

            if (lineLength > maxLength) // maxla current length kýyasý
            {
                direction = direction.normalized * maxLength;
                worldPoint = transform.position + direction;
            }

            Vector3[] positions = { transform.position, worldPoint };
            lineRenderer.SetPositions(positions);

            for (int i = 0; i < positions.Length; i++) // yukarýda aldýðýmýz positionlarý looplama
            {
                positions[i].y = gameObject.transform.position.y+.02f; // lineýn y axisi fixleme
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
        isIdle = true; 
    }
}
