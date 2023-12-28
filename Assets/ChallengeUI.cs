using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private GameObject tryme1, tryme2, tryme3, tryme4, tryme5, tryme6, tryme7, tryme8, tryme9, tryme10, tryme11, tryme12, tryme13, tryme14, tryme15, tryme16, tryme17, tryme18;

    void Start()
    {
        if (PlayerPrefs.GetInt("Cha1") == 1)
        {
            tryme1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha2") == 1)
        {
            tryme2.SetActive(false);
        }
    }
}
