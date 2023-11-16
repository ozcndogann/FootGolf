using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;

public class UIFriend : MonoBehaviour
{
    [SerializeField] private TMP_Text friendNameText;
    [SerializeField] private FriendInfo friend;
    public static Action<string> OnRemoveFriend = delegate { };
    public Image isFriendImage;
    
    public void Initialize(FriendInfo friend)
    {
        this.friend = friend;
        friendNameText.SetText(this.friend.UserId);
    }
    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friend.UserId);
    }
}
