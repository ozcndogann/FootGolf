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
        isChallenge = true;
    }
    void Update()
    {
        
    }
    public void CreateAndJoinRoom()
    {
        StartCoroutine(DelayCreateAndJoinRoom(.1f));
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
        if (ChallengeType.cha1)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
        else if (ChallengeType.cha2)
        {
            PhotonNetwork.LoadLevel("Hole1");
        }
    }

}
