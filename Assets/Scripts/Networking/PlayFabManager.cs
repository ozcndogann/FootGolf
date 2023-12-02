using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour
{
    public GameObject nameWindow;
    public TMP_InputField nameInput;
    public TMP_Text userName;
    public GameObject LeaderBoardPanel;
    public GameObject rowPrefab;
    public Transform rowsParent;
    public GameObject firstAward;
    public GameObject secondAward;
    public GameObject thirdAward;
    string loggedInPlayedId;
    public static bool nameAccepter, playerFound;
    public GameObject Long, Short, Taken,Empty;
    private int LeaguePosition;
    public static string[] friendDisplayNames;

    // Start is called before the first frame update
    void Start()
    {
        LeaguePosition = 0;
        SendLeaderboard(PlayerPrefs.GetInt("Score"));
        PlayerPrefs.GetString("Username");
        playerFound = false;
        nameAccepter = false;
        Login();
    }
    // Update is called once per frame
    private void Update()
    {
        userName.text = PlayerPrefs.GetString("Username");
        //Debug.Log(nameInput.text.Length);
        if (nameAccepter == true)
        {
            nameWindow.SetActive(true);
            nameAccepter = false;
        }
        friendDisplayNames = PlayfabFriendController.friends.Select(f => f.TitleDisplayName).ToArray();
        Debug.Log(friendDisplayNames[0]);
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
        loggedInPlayedId = result.PlayFabId;
        Debug.Log("Successful");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            PlayerPrefs.SetString("Username", name);
            PhotonNetwork.LocalPlayer.NickName = name;  // Set the Photon nickname
            userName.text = name;
        }
        if (name == null)
        {
            nameAccepter = true;
        }

    }
    public void SubmitNameButton()
    {
        string userInput = nameInput.text.Trim();
        if (!string.IsNullOrEmpty(userInput) && userInput.Length >= 3 && userInput.Length <= 8)
        {
            
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameInput.text,

            };
         
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }
        else if (userInput.Length > 8)
        {
            Long.SetActive(true);
            Short.SetActive(false);
            Empty.SetActive(false);
            Taken.SetActive(false);

        }
        else if (string.IsNullOrEmpty(userInput))
        {
            Empty.SetActive(true);
            Long.SetActive(false);
            Short.SetActive(false);
            Taken.SetActive(false);
        }
        else if (userInput.Length < 3)
        {
            Short.SetActive(true);
            Empty.SetActive(false);
            Long.SetActive(false);
            Taken.SetActive(false);
        }
        else
        {
            Taken.SetActive(true);
            Short.SetActive(false);
            Empty.SetActive(false);
            Long.SetActive(false);
        }
       
       
        
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        nameWindow.SetActive(false);
        Debug.Log("NameUpdated");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.Log(error.GenerateErrorReport());
    }
    public void PopCloser()
    {
        Taken.SetActive(false);
        Short.SetActive(false);
        Empty.SetActive(false);
        Long.SetActive(false);
    }
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="FootGolf Leaderboard",
                    Value=score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }
    public void GetLeaderboard()
    {
        LeaderBoardPanel.SetActive(true);
        var request = new GetLeaderboardRequest
        {
            StatisticName = "FootGolf Leaderboard",
            StartPosition = 0,
            MaxResultsCount = 11
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    public void GetLeaderboardLeague()
    {
        LeaguePosition = 0;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "FootGolf Leaderboard",
            StartPosition = 0,
            MaxResultsCount = 11
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardLeagueGet, OnError);
    }
    public void GetLeaderboardFriends()
    {
        LeaguePosition = 0;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "FootGolf Leaderboard",
            StartPosition = 0,
            MaxResultsCount = 11
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardFriendsGet, OnError);
    }
    void OnLeaderboardLeagueGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            if (item.StatValue < 50 && PlayerPrefs.GetInt("Score") < 50)
            {
                LeaguePosition += 1;
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (LeaguePosition).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();
                Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
                if (item.PlayFabId == loggedInPlayedId)
                {
                    playerFound = true;
                    texts[0].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[1].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[2].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                }
                if (LeaguePosition == 1)
                {
                    GameObject newAward = Instantiate(firstAward, texts[0].transform);
                }
                if (LeaguePosition == 2)
                {
                    GameObject newAward = Instantiate(secondAward, texts[0].transform);
                }
                if (LeaguePosition == 3)
                {
                    GameObject newAward = Instantiate(thirdAward, texts[0].transform);
                }
            }
            else if (50 < item.StatValue && item.StatValue < 100 && 50 < PlayerPrefs.GetInt("Score") && PlayerPrefs.GetInt("Score") < 100)
            {
                LeaguePosition += 1;
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (LeaguePosition).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();
                Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
                if (item.PlayFabId == loggedInPlayedId)
                {
                    playerFound = true;
                    texts[0].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[1].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[2].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                }
                if (LeaguePosition == 1)
                {
                    GameObject newAward = Instantiate(firstAward, texts[0].transform);
                }
                if (LeaguePosition == 2)
                {
                    GameObject newAward = Instantiate(secondAward, texts[0].transform);
                }
                if (LeaguePosition == 3)
                {
                    GameObject newAward = Instantiate(thirdAward, texts[0].transform);
                }
            }
            else
            {
                if (playerFound == false)
                {
                    Debug.Log("getaroundcart");
                    GetLeaderboardAroundPlayer();
                }
            }

        }


    }
    void OnLeaderboardFriendsGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            if (friendDisplayNames.Contains(item.DisplayName) || item.PlayFabId == loggedInPlayedId)
            {
                LeaguePosition += 1;
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (LeaguePosition).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();
                Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
                if (item.PlayFabId == loggedInPlayedId)
                {
                    playerFound = true;
                    texts[0].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[1].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[2].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                }
                if (LeaguePosition == 1)
                {
                    GameObject newAward = Instantiate(firstAward, texts[0].transform);
                }
                if (LeaguePosition == 2)
                {
                    GameObject newAward = Instantiate(secondAward, texts[0].transform);
                }
                if (LeaguePosition == 3)
                {
                    GameObject newAward = Instantiate(thirdAward, texts[0].transform);
                }
            }
            else
            {
                if (playerFound == false)
                {
                    Debug.Log("getaroundcart");
                    GetLeaderboardAroundPlayer();
                }
            }

        }


    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            if (item.Position < 10)
            {
                
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();
                Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
                if (item.PlayFabId == loggedInPlayedId)
                {
                    playerFound = true;
                    texts[0].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[1].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                    texts[2].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                }
                if (item.Position == 0) 
                {
                    GameObject newAward = Instantiate(firstAward, texts[0].transform);
                }
                if (item.Position == 1) 
                {
                    GameObject newAward = Instantiate(secondAward, texts[0].transform);
                }
                if (item.Position == 2) 
                {
                    GameObject newAward = Instantiate(thirdAward, texts[0].transform);
                }
            }
            else
            {
                if (playerFound == false)
                {
                    Debug.Log("getaroundcart");
                    GetLeaderboardAroundPlayer();
                }
            }

        }


    }
    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsParent)
        {
            //Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            TMP_Text[] texts = newGo.GetComponentsInChildren<TMP_Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            if (item.PlayFabId == loggedInPlayedId)
            {
                texts[0].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                texts[1].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
                texts[2].color = new Color(97 / 255f, 56 / 255f, 253 / 255f);
            }
            Debug.Log(item.Position + " " + item.DisplayName + " " + item.StatValue);
        }
    }
    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "FootGolf Leaderboard",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }
    public void CloseLeaderboardPanel() 
    {
        LeaderBoardPanel.SetActive(false);
        LeaguePosition = 0;
    }
}