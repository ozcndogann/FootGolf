using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector3 mousePos,upForce;
    private void Awake()
    {
        shootCloser=false;
        rb = GetComponent<Rigidbody>(); 
        isAiming = false; 
        lineRenderer.enabled = false; // ba�ta line g�r�nmemesi i�in
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < stopVelocity) // topun durmas� i�in h�z kontrol�
        {
            Stop(); 
            ProcessAim();
        }
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
                if (mousePos.x > 580)
                {
                    if (mousePos.y > 1180)
                    {
                        Shoot(worldPoint.Value, CurveDirection.LeftDown); // shoot
                    }
                    if (mousePos.y < 1180)
                    {
                        Shoot(worldPoint.Value, CurveDirection.LeftUp); // shoot
                    }
                }
                if (mousePos.x < 580)
                {
                    if (mousePos.y > 1180)
                    {
                        Shoot(worldPoint.Value, CurveDirection.RightDown); // shoot
                    }
                    if (mousePos.y < 1180)
                    {
                        Shoot(worldPoint.Value, CurveDirection.RightUp); // shoot
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
        
        if (Input.GetMouseButtonUp(0)) // parma��m� �ektim mi
        {
            shooted = true;
            Zoom.changeFovBool = true;
            
            //moveAroundObject.heightWhileShooting = 0.3f;
            //cam.transform.position = new Vector3(2 * this.transform.position.x-LineRenderer.transform.position.x, 0.33f, 2 * this.transform.position.z - LineRenderer.transform.position.z);

        }
    }

    //private void Shoot(Vector3 worldPoint)
    //{
    //    isAiming = false; 
    //    lineRenderer.enabled = false; // shoot �al���nca line g�r�nmez olmal�
    //    Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z); // topun yerden gitmesi i�in y axisi ignorela *********

    //    Vector3 direction = (horizontalWorldPoint - transform.position).normalized; //  toptan world pointe y�n� hesapla
    //    float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint); // toptan world pointe mesafeyi hesapla
    //    float force = Mathf.Min(lineLength, 1f) * shotPower; // topun gidi� g�c� line lengthe g�re holacak

    //    rb.AddForce(-direction * force); // �ekti�im y�n�n tersine gitmesi i�in -direction
    //    isIdle = false;
    //}
    //private void Shoot(Vector3 worldPoint)
    //{
    //    isAiming = false;
    //    lineRenderer.enabled = false;

    //    Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);
    //    Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
    //    float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint);
    //    float force = Mathf.Min(lineLength, 1f) * shotPower;

    //    // Add curve to the direction vector
    //    float curveAmount = 0.5f; // Adjust this value to control the curve strength
    //    Vector3 curveDirection = Quaternion.AngleAxis(-90f, Vector3.up) * direction; // Apply a left curve
    //    Vector3 finalDirection = direction + curveAmount * curveDirection;

    //    rb.AddForce(-finalDirection * force);
    //    isIdle = false;
    //    shooted = false;
    //}
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
                curveVector = Quaternion.AngleAxis(-90f, Vector3.up) * direction;
                upForce = new Vector3(0, 0.6f, 0);
                break;
            case CurveDirection.LeftDown:
                curveVector = Quaternion.AngleAxis(-90f, Vector3.up) * direction;
                break;
            case CurveDirection.RightUp:
                curveVector = Quaternion.AngleAxis(90f, Vector3.up) * direction;
                upForce = new Vector3(0, 0.6f, 0);
                break;
            case CurveDirection.RightDown:
                curveVector = Quaternion.AngleAxis(90f, Vector3.up) * direction;
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
            float maxLength = 1f; // max line length

            if (lineLength > maxLength) // maxla current length k�yas�
            {
                direction = direction.normalized * maxLength;
                worldPoint = transform.position + direction;
            }

            Vector3[] positions = { transform.position, worldPoint };
            lineRenderer.SetPositions(positions);

            for (int i = 0; i < positions.Length; i++) // yukar�da ald���m�z positionlar� looplama
            {
                positions[i].y = 0.28f; // line�n y axisi fixleme
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
}
