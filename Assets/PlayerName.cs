using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
public class PlayerName : MonoBehaviour
{
    public TMP_Text nicknameText;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        nicknameText.text = view.Owner.NickName;
    }

    void Update()
    {
        //nickname always faces the camera
        if (nicknameText != null)
        {
            nicknameText.transform.forward = Camera.main.transform.forward;
        }
    }
}
