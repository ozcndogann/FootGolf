//GRÝD BASED
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Ball;
    public float Y; 
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
            int index = PhotonNetwork.LocalPlayer.ActorNumber % spawnPositions.Count;
            Vector3 spawnPos = spawnPositions[index];
            PhotonNetwork.Instantiate(Ball.name, spawnPos, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(Ball.name, spawnPositions[0], Quaternion.identity);
        }
    }

    void GenerateSpawnPositions()
    {
        float spacingX = (maxX - minX) / 4; 
        float middleZ = (minZ + maxZ) / 2; 

        for (int i = 0; i < 4; i++)
        {
            float xPosition = minX + (spacingX * i);
            float zPosition = (i % 2 == 0) ? minZ : middleZ; 
            spawnPositions.Add(new Vector3(xPosition, Y, zPosition));
        }
    }
}


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
