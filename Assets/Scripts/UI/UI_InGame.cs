using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class UI_InGame : MonoBehaviour
{
    [SerializeField] public TMP_Text codeText;
    public GameObject ReturnPanel;
    
    public void Start()
    {
        codeText.text = CreateAndJoinRooms.randomCreate;

        if (CreateAndJoinRooms.practice)
        {
            codeText.enabled = false;
        }
        else if (CreateAndJoinRooms.versus)
        {
            codeText.enabled = true;
        }
        else if (CreateAndJoinRooms.Tournament)
        {
            codeText.enabled = true;
        }
    }
    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length < 2 && CreateAndJoinRandomRooms.versus)
        {
            //eksik kisi ui
            Debug.Log("eksik kisi");
            ReturnPanel.SetActive(true);
        }
        else if (PhotonNetwork.PlayerList.Length >= 3 && CreateAndJoinRandomRooms.Tournament)
        {
            //eksik kisi
            Debug.Log("eksik kisi");
            ReturnPanel.SetActive(true);
        }
        else
        {
            ReturnPanel.SetActive(false);
        }
    }
    public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
