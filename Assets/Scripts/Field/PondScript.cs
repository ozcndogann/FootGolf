using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondScript : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody rb;
    public Transform ballTransform;
    Vector3 contactPoint;
    Vector3 teleportPoint;
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = ball.GetComponent<Rigidbody>();
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
        ballTransform.position = new Vector3(teleportPoint.x, teleportPoint.y+0.1f/*gameObject.transform.position.y+1.26f*/, teleportPoint.z);
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            contactPoint = other.ClosestPointOnBounds(transform.position);
            teleportPoint = CalculateTeleportPoint(contactPoint, other.bounds);

            Debug.Log("Contact Point: " + contactPoint);

            StartCoroutine(waiter());
        }
    }

    private Vector3 CalculateTeleportPoint(Vector3 contactPoint, Bounds colliderBounds)
    {
        Vector3 closestPointOnBounds = colliderBounds.ClosestPoint(contactPoint);
        Vector3 direction = (closestPointOnBounds - contactPoint).normalized;

        Vector3 horizontalDirection = new Vector3(direction.x, 0f, direction.z).normalized;

        float distance = 0.9f; // Distance to move outside the bounds
        Vector3 teleportPoint = closestPointOnBounds + horizontalDirection * distance;

        return teleportPoint;
    }
}
