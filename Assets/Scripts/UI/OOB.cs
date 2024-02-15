using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OOB : MonoBehaviour
{
    Animator animator;
    Image image;

    private void Start()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void OOBFunc()
    {
        IEnumerator OOB()
        {
            yield return new WaitForSeconds(1.3f);

            image.preserveAspect = true;
            animator.SetBool("oob", false);
            image.enabled = false;
        }
        image.enabled = true;
        animator.SetBool("oob", true);
        StartCoroutine(OOB());
    }
}
