using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;
using UnityEngine;

public class PlayfabFriendController : MonoBehaviour
{
    public static Action<List<FriendInfo>> OnFriendListUpdated = delegate { };
    private List<FriendInfo> friends;
    bool seeList;
    private void Awake()
    {
        friends = new List<FriendInfo>();
        ProfileName.GetPhotonFriends += HandleGetFriends;
        UIAddFriend.OnAddFriend += HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend += HandleRemoveFriend;
    }
    private void OnDestroy()
    {
        ProfileName.GetPhotonFriends -= HandleGetFriends;
        UIAddFriend.OnAddFriend -= HandleAddPlayfabFriend;
        UIFriend.OnRemoveFriend -= HandleRemoveFriend;
    }
    private void HandleAddPlayfabFriend(string name)
    {
        Debug.Log($"Playfab add friend request for {name}");
        var request = new AddFriendRequest { FriendTitleDisplayName = name };
        PlayFabClientAPI.AddFriend(request, OnFriendAddedSuccess, OnFailure);
    }
    private void HandleRemoveFriend(string name)
    {
        string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
        Debug.Log($"Playfab remove friend {name} with id {id}");
        var request = new RemoveFriendRequest { FriendPlayFabId = id };
        PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, OnFailure);
    }

    private void HandleGetFriends()
    {
        GetPlayfabFriends();
    }

    private void GetPlayfabFriends()
    {
        Debug.Log("Playfab get friend list request");
        var request = new GetFriendsListRequest { /*IncludeSteamFriends = false, IncludeFacebookFriends = false,*/ XboxToken = null };
        PlayFabClientAPI.GetFriendsList(request, OnFriendsListSuccess, OnFailure);
    }
    private void OnFriendAddedSuccess(AddFriendResult result)
    {
        Debug.Log("Playfab add friend success getting updated friend list");
        GetPlayfabFriends();
    }

    private void OnFriendsListSuccess(GetFriendsListResult result)
    {
        Debug.Log($"Playfab get friend list success: {result.Friends.Count}");
        friends = result.Friends;
        OnFriendListUpdated?.Invoke(result.Friends);
    }

    private void OnFriendRemoveSuccess(RemoveFriendResult result)
    {
        Debug.Log($"Playfab remove friend success");
        GetPlayfabFriends();
    }

    private void OnFailure(PlayFabError error)
    {
        Debug.Log($"Playfab Friend Error occured: {error.GenerateErrorReport()}");
    }
}