using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileName : MonoBehaviour
{
    public TMP_Text userName;
    public static Action GetPhotonFriends = delegate { };
    void Start()
    {
        userName.text = PlayerPrefs.GetString("Username");
        GetPhotonFriends?.Invoke();
    }
}
