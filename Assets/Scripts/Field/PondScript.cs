using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondScript : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody rb;
    //public Transform ballTransform;
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
        ball.transform.position = new Vector3(teleportPoint.x, gameObject.transform.position.y+1.26f, teleportPoint.z);
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            contactPoint = other.ClosestPointOnBounds(transform.position);
            teleportPoint = CalculateTeleportPoint(contactPoint, transform.position, transform.localScale.x / 2f);
            Debug.Log("Contact Point: " + contactPoint);

            StartCoroutine(waiter());
        }
    }

    private Vector3 CalculateTeleportPoint(Vector3 contactPoint, Vector3 sphereCenter, float sphereRadius)
    {
        Vector3 direction = contactPoint - sphereCenter;
        direction.y = 0f; // Ignore vertical component

        Vector3 normalizedDirection = direction.normalized;

        Vector3 teleportPoint = contactPoint + normalizedDirection * sphereRadius;

        return teleportPoint;
    }
}
