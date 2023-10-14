using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    public int ShotCount;
    void Start()
    {
    }

    void Update()
    {
        
    }
    [PunRPC]
    public void UpdateShotCount(int count)
    {
        ShotCount = count;
        // Update UI or do other tasks
    }
}
