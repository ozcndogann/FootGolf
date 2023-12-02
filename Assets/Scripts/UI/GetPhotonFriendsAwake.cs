using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GetPhotonFriendsAwake : MonoBehaviour
{
    public static Action GetPhotonFriends = delegate { };
    void Start()
    {
        GetPhotonFriends?.Invoke();
    }
}
