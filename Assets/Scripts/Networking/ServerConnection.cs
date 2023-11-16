using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text warningConnectionFailed;
    private void Start()
    {
        //PhotonNetwork.GameVersion = "1.0.2";
        PhotonNetwork.ConnectUsingSettings();
        Login();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        LoadScene("MainMenu");
       
    }
    public async void LoadScene(string SceneName)
    {
        var scene = SceneManager.LoadSceneAsync(SceneName);
        scene.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
            //şimdilik slider'ı kapattım sonra scene progrese göre yaparsak animation'a değer verererk yaparız
            //LoadBar.value = scene.progress;
        }
        while (scene.progress < 0.9f);
        await Task.Delay(100);
        scene.allowSceneActivation = true;
    }
    
   
    public override void OnDisconnected(DisconnectCause cause)
    {
        //burda debug yerine bi pop up veririz
        warningConnectionFailed.text = "Disconnected from server because: " + cause.ToString();
    }
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);

    }
    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful");
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.Log(error.GenerateErrorReport());
    }
}
