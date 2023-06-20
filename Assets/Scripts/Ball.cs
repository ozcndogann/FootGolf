using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Force;
    public Transform Target;
    private bool isGrounded;
    private Vector3 startPos;
    private Vector3 endPos;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        // Check if object is standing on the ground and speed is zero
        if (isGrounded && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 Shoot = (Target.position - this.transform.position).normalized;
        rb.AddForce(Shoot * Force, ForceMode.Impulse);
    }
}
