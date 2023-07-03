using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private bool isIdle;
    private bool isAiming;
    [SerializeField] private float stopVelocity;
    [SerializeField] private float shotPower;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isAiming = false;
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            Stop();
        }
        ProcessAim();
    }
    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    //private void ProcessAim() 
    //{
    //    if (!isAiming || !isIdle)
    //    {
    //        return;
    //    }

    //    Vector3? worldPoint = CastMouseClickRay();
    //    if (!worldPoint.HasValue)
    //    {
    //        return;
    //    }
    //    DrawLine(worldPoint.Value);
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        Shoot(worldPoint.Value);
    //    }
    //}

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();
        if (!worldPoint.HasValue)
        {
            return;
        }
        DrawLine(transform.position - (worldPoint.Value - transform.position)); // Invert the line drawing direction
        if (Input.GetMouseButtonUp(0))
        {
            Shoot(worldPoint.Value);
        }
    }


    private void Shoot(Vector3 worldPoint)
    {
        isAiming = false;
        lineRenderer.enabled = false;
        Vector3 horizontalWorldPoint = new Vector3(worldPoint.x, transform.position.y, worldPoint.z);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float lineLength = Vector3.Distance(transform.position, horizontalWorldPoint);
        float force = Mathf.Min(lineLength, 1f) * shotPower; // Limit the line length to a maximum of 1 and scale it by the shotPower

        rb.AddForce(-direction * force);
        isIdle = false;
    }



    //private void DrawLine(Vector3 worldPoint)
    //{
    //    Vector3[] positions = { transform.position, worldPoint};
    //    lineRenderer.SetPositions(positions);
    //    lineRenderer.enabled = true;
    //}

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3 direction = worldPoint - transform.position;
        float lineLength = direction.magnitude;
        float maxLength = 1f; // Define your desired maximum length here

        if (lineLength > maxLength)
        {
            direction = direction.normalized * maxLength;
            worldPoint = transform.position + direction;
        }

        Vector3[] positions = { transform.position, worldPoint };
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
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
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, float.PositiveInfinity))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }
    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isIdle = true;
    }
}
