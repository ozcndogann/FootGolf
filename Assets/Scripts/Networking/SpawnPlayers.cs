using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Ball;
    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;
    public float Y;
    private float radius; // Remove the default assignment

    private void Start()
    {
        radius = Ball.GetComponent<SphereCollider>().radius;

        Vector3 startPos = GetRandomClearPosition();

        if (startPos != Vector3.zero) // If we found a clear position
        {
            PhotonNetwork.Instantiate(Ball.name, startPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomClearPosition()
    {
        int maxAttempts = 100; // Number of attempts to find a clear spot
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Y, Random.Range(minZ, maxZ));

            if (IsPositionClear(randomPos))
            {
                return randomPos;
            }
        }

        return Vector3.zero; // Return Vector3.zero if no clear position found after max attempts
    }

    bool IsPositionClear(Vector3 position)
    {
        // Checks if there are any colliders overlapping the position within the specified radius
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player")) // Make sure to tag your player/Ball objects with the "Player" tag or any other appropriate tag
            {
                return false;
            }
        }

        return true;
    }
}





//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//public class SpawnPlayers : MonoBehaviour
//{
//    public GameObject Ball;
//    public float minX;
//    public float minZ;
//    public float maxX;
//    public float maxZ;
//    public float Y;
//    private void Start()
//    {
//        Vector3 startPos = new Vector3(Random.Range(minX, maxX), Y, Random.Range(minZ, maxZ));
//        PhotonNetwork.Instantiate(Ball.name, startPos, Quaternion.identity);
//    }
//}
