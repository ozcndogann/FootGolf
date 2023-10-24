//GRÝD BASED
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Ball;
    public float Y; // Your consistent Y value
    public float minZ;
    public float maxZ;
    public float minX;
    public float maxX;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private void Start()
    {
        GenerateSpawnPositions();

        if (PhotonNetwork.IsConnected)
        {
            // Use the player's actor number to get a unique spawn position
            int index = PhotonNetwork.LocalPlayer.ActorNumber % spawnPositions.Count;
            Vector3 spawnPos = spawnPositions[index];
            PhotonNetwork.Instantiate(Ball.name, spawnPos, Quaternion.identity);
        }
        else
        {
            // For non-networked testing, just spawn the ball at the first position
            PhotonNetwork.Instantiate(Ball.name, spawnPositions[0], Quaternion.identity);
        }
    }

    void GenerateSpawnPositions()
    {
        float spacingX = (maxX - minX) / 3; // Since you want 4 grids, we divide by 3 to get the space between them
        float middleZ = (minZ + maxZ) / 2; // Assuming 2 spawn positions on each side of the Z axis

        for (int i = 0; i < 4; i++)
        {
            float xPosition = minX + (spacingX * i);
            float zPosition = (i % 2 == 0) ? minZ : middleZ; // Alternates between minZ and middleZ
            spawnPositions.Add(new Vector3(xPosition, Y, zPosition));
        }
    }
}




//FÝZÝÐE GÖRE ÇAKIÞIYOSA YENÝ POZÝSYON BULUYOR AMA ÇALIÞMADI

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
//    private float radius; // Remove the default assignment

//    private void Start()
//    {
//        radius = Ball.GetComponent<SphereCollider>().radius;

//        Vector3 startPos = GetRandomClearPosition();

//        if (startPos != Vector3.zero) // If we found a clear position
//        {
//            PhotonNetwork.Instantiate(Ball.name, startPos, Quaternion.identity);
//        }
//    }

//    Vector3 GetRandomClearPosition()
//    {
//        int maxAttempts = 100; // Number of attempts to find a clear spot
//        for (int i = 0; i < maxAttempts; i++)
//        {
//            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Y, Random.Range(minZ, maxZ));

//            if (IsPositionClear(randomPos))
//            {
//                return randomPos;
//            }
//        }

//        return Vector3.zero; // Return Vector3.zero if no clear position found after max attempts
//    }

//    bool IsPositionClear(Vector3 position)
//    {
//        // Checks if there are any colliders overlapping the position within the specified radius
//        Collider[] colliders = Physics.OverlapSphere(position, radius);

//        foreach (Collider collider in colliders)
//        {
//            if (collider.gameObject.CompareTag("Player")) // Make sure to tag your player/Ball objects with the "Player" tag or any other appropriate tag
//            {
//                return false;
//            }
//        }

//        return true;
//    }
//}



//OLD

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
