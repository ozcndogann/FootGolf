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
        }
    }
    IEnumerator LoadScene()
    {
        Animator.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
    }
    
}
