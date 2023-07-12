using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
        isAiming = false; 
        lineRenderer.enabled = false; // baþta line görünmemesi için
        moveAroundObject = cam.GetComponent<MoveAroundObject>();
        zoom = cam.GetComponent<Zoom>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < stopVelocity) // topun durmasý için hýz kontrolü
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
    }
    
    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return; // exit method
        }

        Vector3? worldPoint = CastMouseClickRay(); // world pointi belirlemek için clickten ray yolla 
        if (!worldPoint.HasValue) // ray bi þeye çarptý mý diye check
        {
            return; // exit method
        }
        DrawLine(transform.position - (worldPoint.Value - transform.position)); // aim line çiz
        
        if (Input.GetMouseButtonUp(0)) // parmaðýmý çektim mi
        {
            //moveAroundObject.heightWhileShooting = 0.3f;
            
            Shoot(worldPoint.Value); // shoot
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false; 
        lineRenderer.enabled = false; // shoot çalýþýnca line görünmez olmalý
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z); // topun yerden gitmesi için y axisi ignorela *********

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized; //  toptan world pointe yönü hesapla
        float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint); // toptan world pointe mesafeyi hesapla
        float force = Mathf.Min(lineLength, 1f) * shotPower; // topun gidiþ gücü line lengthe göre holacak

        rb.AddForce(-direction * force); // çektiðim yönün tersine gitmesi için -direction
        isIdle = false; 
    }

    private void DrawLine(Vector3 worldPoint)
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
            positions[i].y = 0.28f; // lineýn y axisi fixleme
        }

        lineRenderer.SetPositions(positions); // update positions
        lineRenderer.enabled = true; // line visible
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
