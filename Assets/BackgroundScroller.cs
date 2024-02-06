using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField] Image bgRenderer;
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
