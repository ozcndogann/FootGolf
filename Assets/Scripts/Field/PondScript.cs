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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Vector3 closestDestination = FindClosestTeleportDestination(other.transform.position);

            if (closestDestination != Vector3.zero)
            {
                // Calculate the teleport direction from the closest destination to the current position
                Vector3 teleportDirection = closestDestination - other.transform.position;
                teleportDirection.y = 0; // Remove vertical component
                teleportDirection.Normalize();

                // Calculate the new teleport position
                Vector3 newPosition = closestDestination + teleportDirection * teleportDistance;

                // Create a ray from the newPosition downward to find the ground
                Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.down); // Start just above to avoid hitting the ball
                RaycastHit hit;

                // Check if the ray hits the ground layer using the layer mask
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask.value))
                {
                    Debug.Log("Hit object layer: " + hit.collider.gameObject.layer);
                    newPosition.y = hit.point.y + 0.2f; // Adjust the height above the ground
                }

                // Teleport the ball
                StartCoroutine(TeleportBall(other.gameObject, newPosition));
            }
        }
    }

    private Vector3 FindClosestTeleportDestination(Vector3 ballPosition)
    {
        Vector3 closestDestination = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject destination in teleportDestinations)
        {
            Vector3 destinationPosition = destination.transform.position;
            float distance = Vector3.Distance(ballPosition, destinationPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDestination = destinationPosition;
            }
        }

        return closestDestination;
    }

    private IEnumerator TeleportBall(GameObject ballToTeleport, Vector3 newPosition)
    {
        yield return new WaitForSeconds(0.15f);

        ballRigidbody.velocity = Vector3.zero; // Stop the ball's velocity
        ballToTeleport.transform.position = newPosition; // Teleport the ball to the new position
    }
}
//private Vector3 CalculateClosestTeleportPosition()
//{
//    Vector3 closestPosition = Vector3.zero;
//    float closestDistance = Mathf.Infinity;

//    foreach (GameObject destination in teleportDestinations)
//    {
//        // secilen destinasyon colliderýndaki en yakýn noktayla topun arasýndaki mesafe
//        Vector3 closestPoint = destination.GetComponent<Collider>().ClosestPoint(ball.transform.position);

//        float distance = Vector3.Distance(closestPoint, ball.transform.position);

//        // bi oncekinden daha mý yakýn diye test et
//        if (distance < closestDistance)
//        {
//            closestDistance = distance;
//            closestPosition = closestPoint;
//        }
//    }

//    return closestPosition;
//}





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

