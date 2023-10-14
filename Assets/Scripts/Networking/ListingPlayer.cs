using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ListingPlayer : MonoBehaviour
{
    public TMP_Text NickName;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        NickName.text = player.NickName;  // Directly set the nickname from the Player object
    }
}