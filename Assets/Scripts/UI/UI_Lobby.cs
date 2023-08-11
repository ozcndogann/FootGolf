using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] private TMP_Text codeText;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        codeText.text = CreateAndJoinRooms.randomCreate;
    }
    public void StartGame()
    {
        //buraya sahalar�n t�rlerine g�re if state gelcek
        
        PhotonNetwork.LoadLevel("Hole1");
    }
}
