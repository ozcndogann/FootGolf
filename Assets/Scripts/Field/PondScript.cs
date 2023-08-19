using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondScript : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody ballRigidbody;
    //public Transform ballTransform;
    public float teleportDistance = 2.0f;
    Vector3 contactPoint;
    Vector3 teleportPoint;
    public LayerMask teleportLayerMask; // teleport noktasý için zemin layerini seçmek
    public GameObject[] teleportDestinations; // teleport destinations diye empty gameobjectlerimiz var


    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    //IEnumerator waiter()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    ball.transform.position = new Vector3(teleportPoint.x, gameObject.transform.position.y + 1.26f, teleportPoint.z);
    //    rb.velocity = new Vector3(0, 0, 0);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Calculate the teleportation position on the lateral surfaces of the destination BoxCollider
            Vector3 newPosition = CalculateClosestTeleportPosition();

            // Teleport the ball to the new position
            ballRigidbody.velocity = Vector3.zero; // Stop the ball's velocity
            ball.transform.position = newPosition;
        }
    }

    private Vector3 CalculateClosestTeleportPosition()
    {
        Vector3 closestPosition = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject destination in teleportDestinations)
        {
            // secilen destinasyon colliderýndaki en yakýn noktayla topun arasýndaki mesafe
            Vector3 closestPoint = destination.GetComponent<Collider>().ClosestPoint(ball.transform.position);

            float distance = Vector3.Distance(closestPoint, ball.transform.position);

            // bi oncekinden daha mý yakýn diye test et
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = closestPoint;
            }
        }

        return closestPosition;
    }





    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        contactPoint = other.ClosestPointOnBounds(transform.position);
    //        teleportPoint = CalculateTeleportPoint(contactPoint, transform.position, transform.localScale.x);
    //        Debug.Log("Contact Point: " + contactPoint);

    //        StartCoroutine(waiter());
    //    }
    //}

    //private Vector3 CalculateTeleportPoint(Vector3 contactPoint, Vector3 sphereCenter, float sphereRadius)
    //{
    //    Vector3 direction = contactPoint - sphereCenter;
    //    direction.y = 0f; // Ignore vertical component

    //    Vector3 normalizedDirection = direction.normalized;

    //    Vector3 teleportPoint = contactPoint + normalizedDirection * sphereRadius;

    //    return teleportPoint;
    //}
}
