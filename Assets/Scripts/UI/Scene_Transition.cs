using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

 class Scene_Transition : MonoBehaviour
{
    public Animator Animator;
    public Button Button;
    public string Scene;

    // Update is called once per frame
    void Update()
    {
        if (Button == true)
        {
            SceneManager.LoadScene(Scene);
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        Animator.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
    }
    
}
