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

    private void Awake()
    {
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
    }
    
    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return; // exit method
        }

        Vector3? worldPoint = CastMouseClickRay(); // world pointi belirlemek i�in clickten ray yolla 
        if (!worldPoint.HasValue) // ray bi �eye �arpt� m� diye check
        {
            return; // exit method
        }
        DrawLine(transform.position - (worldPoint.Value - transform.position)); // aim line �iz
        
        if (Input.GetMouseButtonUp(0)) // parma��m� �ektim mi
        {
            //moveAroundObject.heightWhileShooting = 0.3f;
            
            Shoot(worldPoint.Value); // shoot
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false; 
        lineRenderer.enabled = false; // shoot �al���nca line g�r�nmez olmal�
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z); // topun yerden gitmesi i�in y axisi ignorela *********

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized; //  toptan world pointe y�n� hesapla
        float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint); // toptan world pointe mesafeyi hesapla
        float force = Mathf.Min(lineLength, 1f) * shotPower; // topun gidi� g�c� line lengthe g�re holacak

        rb.AddForce(-direction * force); // �ekti�im y�n�n tersine gitmesi i�in -direction
        isIdle = false; 
    }

    private void DrawLine(Vector3 worldPoint)
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
