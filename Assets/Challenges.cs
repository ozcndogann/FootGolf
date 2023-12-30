using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;
public class Challenges : MonoBehaviourPunCallbacks
{
    public static RoomOptions roomOptions = new RoomOptions();
    public static string randomCreate;
    public static bool isChallenge;
    ChallengeType chatype;

    void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            isChallenge = true;
        }
        else
        {
            isChallenge = false;
        }
    }
    void Update()
    {
        
    }
    public void CreateAndJoinRoom()
    {
        StartCoroutine(DelayCreateAndJoinRoom(.1f));
    }
    public void RetryCreateAndJoinRoom()
    {
        Ball.challangeCheck = false;
        ShotCounter.ShotCount = 0;
        PhotonNetwork.LeaveRoom();
        StartCoroutine(DelayCreateAndJoinRoom(.5f));
    }
    private string GenerateRandomSixDigitNumber()
    {
        int randomNumber = Random.Range(100000, 1000000);
        return randomNumber.ToString();
    }
    
    private IEnumerator DelayCreateAndJoinRoom(float delay)
    {
        yield return new WaitForSeconds(delay);
        randomCreate = GenerateRandomSixDigitNumber();
        roomOptions.CustomRoomProperties = new PhotonHashTable();
        roomOptions.CustomRoomProperties.Add("GameModeChallange", Switch.index);
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "GameModeChallange" };
        roomOptions.MaxPlayers = 1;
        PhotonNetwork.CreateRoom(randomCreate, roomOptions);
    }
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Score"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Score", 0 } });
        }
        #region LoadHoles
        if (ChallengeType.cha1)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (ChallengeType.cha2)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (ChallengeType.cha3)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (ChallengeType.cha4)
        {
            PhotonNetwork.LoadLevel("Hole2");
        }
        else if (ChallengeType.cha5)
        {
            PhotonNetwork.LoadLevel("Hole2");
        }
        else if (ChallengeType.cha6)
        {
            PhotonNetwork.LoadLevel("Hole2");
        }
        else if (ChallengeType.cha7)
        {
            PhotonNetwork.LoadLevel("Hole3Rainy");
        }
        else if (ChallengeType.cha8)
        {
            PhotonNetwork.LoadLevel("Hole3Rainy");
        }
        else if (ChallengeType.cha9)
        {
            PhotonNetwork.LoadLevel("Hole3Rainy");
        }
        else if (ChallengeType.cha10)
        {
            PhotonNetwork.LoadLevel("Hole5Rainy");
        }
        else if (ChallengeType.cha11)
        {
            PhotonNetwork.LoadLevel("Hole5Rainy");
        }
        else if (ChallengeType.cha12)
        {
            PhotonNetwork.LoadLevel("Hole5Rainy");
        }
        else if (ChallengeType.cha13)
        {
            PhotonNetwork.LoadLevel("Hole6");
        }
        else if (ChallengeType.cha14)
        {
            PhotonNetwork.LoadLevel("Hole6");
        }
        else if (ChallengeType.cha15)
        {
            PhotonNetwork.LoadLevel("Hole6");
        }
        else if (ChallengeType.cha16)
        {
            PhotonNetwork.LoadLevel("Hole7Rainy");
        }
        else if (ChallengeType.cha17)
        {
            PhotonNetwork.LoadLevel("Hole7Rainy");
        }
        else if (ChallengeType.cha18)
        {
            PhotonNetwork.LoadLevel("Hole7Rainy");
        }
        #endregion
    }

}
