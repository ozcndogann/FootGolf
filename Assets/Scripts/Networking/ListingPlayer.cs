using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class ListingPlayer : MonoBehaviour
{
    [SerializeField] private PlayFabManager _text;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {


        Player = player;

          player.NickName =  _text.name;
    }



}
