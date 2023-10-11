using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ListingPlayer : MonoBehaviour
{
    [SerializeField] private PlayFabManager _text;
    public TMP_Text NickName;

    public Player Player { get; private set; }
    public void Start()
    {
        NickName.text = PlayerPrefs.GetString("Username");
    }
    public void SetPlayerInfo(Player player)
    {

       
        Player = player;

          player.NickName =  _text.name;
        
    }



}
