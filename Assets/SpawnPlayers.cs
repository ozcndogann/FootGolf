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
    private void Start()
    {
        Vector3 startPos = new Vector3(Random.Range(minX, maxX), .3f, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(Ball.name, startPos, Quaternion.identity);
    }
}
