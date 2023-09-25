using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Start : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene("LobbyOrQuick");
        StartCoroutine(DelayCheck(.5f));
    }
    public void Practice()
    {
        SceneManager.LoadScene("Hole1");
    }
    private IEnumerator DelayCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LobbyOrQuick");
    }

}
