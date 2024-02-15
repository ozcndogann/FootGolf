using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Start : MonoBehaviour
{
    private void Start()
    {
        //AudioManager.Instance.PlayMusic("Theme");
    }
    public void PlayGame()
    {
        //SceneManager.LoadScene("LobbyOrQuick");
        StartCoroutine(DelayCheck(.5f));
    }
    public void Practice()
    {
        //SceneManager.LoadScene("UIRandomGameSelect");
        StartCoroutine(DelayCheck2(.5f));
    }
    private IEnumerator DelayCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("UIGameSelect");
    }
    private IEnumerator DelayCheck2(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Challanges");
    }
    public void ProfileScene()
    {
        SceneManager.LoadScene("Profile");
        LevelLoader.LoaderInstance.FadeToLevel("Profile");
    }
}
