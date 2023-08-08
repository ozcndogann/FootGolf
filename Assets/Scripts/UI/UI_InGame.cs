using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class UI_InGame : MonoBehaviour
{
    [SerializeField] private TMP_Text codeText;
    
    private void Start()
    {
        codeText.text = CreateAndJoinRooms.randomCreate;
    }
    public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
