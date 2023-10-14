using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public static class PhotonExtensions
{
    public static GameObject GetPlayerGameObject(Player player)
    {
        PhotonView[] photonViews = Object.FindObjectsOfType<PhotonView>();
        foreach (var pv in photonViews)
        {
            if (pv.Owner == player)
            {
                return pv.gameObject;
            }
        }
        return null; // return null if no GameObject is found for the given player
    }
}
