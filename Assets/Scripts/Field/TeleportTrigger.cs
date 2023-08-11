using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    public float teleportDistance = 2.0f; // Distance from the wall where the ball will be teleported
    public LayerMask teleportLayerMask; // Layer mask to specify where the ball can be teleported

    private void Start()
    {
        ballRigidbody = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Calculate the teleportation position (from the wall towards the trigger point)
            Vector3 wallPosition = transform.position;
            Vector3 triggerPoint = other.ClosestPoint(wallPosition);
            Vector3 teleportDirection = wallPosition - triggerPoint; // Reverse the direction
            Vector3 newPosition = triggerPoint + teleportDirection.normalized * teleportDistance;

            // Cast a ray downwards to find the ground position
            Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.down); // Start slightly above to avoid hitting itself
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                newPosition.y = hit.point.y+ 0.2f; // Set the teleport position to the ground height
            }

            // Teleport the ball to the new position
            ballRigidbody.velocity = Vector3.zero; // Stop the ball's velocity
            other.transform.position = newPosition;
        }
    }
}
