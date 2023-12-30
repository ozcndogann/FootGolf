using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private GameObject tryme1, tryme2, tryme3, tryme4, tryme5, tryme6, tryme7, tryme8, tryme9, tryme10, tryme11, tryme12, tryme13, tryme14, tryme15, tryme16, tryme17, tryme18;

    void Start()
    {
        if (PlayerPrefs.GetInt("Cha1") == 2)
        {
            tryme1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha2") == 2)
        {
            tryme2.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha3") == 2)
        {
            tryme3.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha4") == 2)
        {
            tryme4.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha5") == 2)
        {
            tryme5.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha6") == 2)
        {
            tryme6.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha7") == 2)
        {
            tryme7.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha8") == 2)
        {
            tryme8.SetActive(false);

        }
        if (PlayerPrefs.GetInt("Cha9") == 2)
        {
            tryme9.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha10") == 2)
        {
            tryme10.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha11") == 2)
        {
            tryme11.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha12") == 2)
        {
            tryme12.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha13") == 2)
        {
            tryme13.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha14") == 2)
        {
            tryme14.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha15") == 2)
        {
            tryme15.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha16") == 2)
        {
            tryme16.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha17") == 2)
        {
            tryme17.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Cha18") == 2)
        {
            tryme18.SetActive(false);
        }
    }
    public void GoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
