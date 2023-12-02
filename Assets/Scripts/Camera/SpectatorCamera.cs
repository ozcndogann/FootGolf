using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorCamera : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        CheckTurn();
    }
    private void CheckTurn()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("turn") && (bool)player.CustomProperties["turn"])
            {
                Debug.Log("It's the turn of player: " + player.NickName);
            }
        }
    }
}
