using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerNameSetter : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerNickname;
    public GameObject Submit,GotIt;
    public GameObject Panel,NameLong;
    public static bool nameAccepter, playerFound;

    public void Start()
    {

        PhotonNetwork.NickName = PlayerNickname.text;
        if(Submit == true)
        {
            this.Panel.SetActive(false);
        }
        PlayerPrefs.GetInt("NameWindowOpen", 0);
        playerFound = false;
        nameAccepter = false;
    }
    private void Update()
    {
        if (nameAccepter == true && PlayerPrefs.GetInt("NameWindowOpen") == 0)
        {
            Panel.SetActive(true);
            nameAccepter = false;
        }
        if(PlayerNickname.text.Length > 6)
        {
            NameLong.SetActive(true);

            if(GotIt == true)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
