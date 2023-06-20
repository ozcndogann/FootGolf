using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Force;
    public float MaxDragDistance; // Maximum allowed drag distance
    private bool isGrounded;
    private bool isDragging;
    private Rigidbody rb;
    private LineRenderer lineRenderer;
    private Vector3 initialMousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        // Check if object is standing on the ground
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isDragging = true;
                initialMousePosition = Input.mousePosition;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);
                lineRenderer.enabled = true;
            }
            else if (isDragging && Input.GetKey(KeyCode.Mouse0))
            {
                UpdateDirection();
            }
            else if (isDragging && Input.GetKeyUp(KeyCode.Mouse0))
            {
                Shoot();
                lineRenderer.enabled = false;
                isDragging = false;
            }
        }
    }

    void UpdateDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 direction = (initialMousePosition - mousePosition).normalized;
        float dragMagnitude = (initialMousePosition - mousePosition).magnitude;

        if (dragMagnitude > MaxDragDistance)
        {
            dragMagnitude = MaxDragDistance;
        }

        lineRenderer.SetPosition(1, transform.position + direction * dragMagnitude / 500);
    }

    void Shoot()
    {
        Vector3 direction = (transform.position - lineRenderer.GetPosition(1)).normalized;
        float dragMagnitude = (initialMousePosition - Input.mousePosition).magnitude;

        if (dragMagnitude > MaxDragDistance)
        {
            dragMagnitude = MaxDragDistance;
        }

        rb.AddForce(-direction * Force * dragMagnitude, ForceMode.Impulse);
    }
}
