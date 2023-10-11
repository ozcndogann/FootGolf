using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NewBehaviourScript1 : MonoBehaviourPunCallbacks
{
    public InputField PlayerNickname;
    public GameObject Submit;
    public GameObject Panel;

    public void GameStart()
    {

        PhotonNetwork.NickName = PlayerNickname.text;
        if (Submit == true)
        {
            this.Panel.SetActive(false);
        }
    }

}
