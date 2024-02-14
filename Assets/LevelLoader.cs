using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader LoaderInstance;
    public Animator Animator;
    private string levelToLoad;
    
    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        Animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
