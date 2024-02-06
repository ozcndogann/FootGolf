using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Texture[] textures;
    private int animationStep;

    [SerializeField]
    private float fps = 3f;
    private float fpsCounter;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.GetComponentInParent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter += Time.deltaTime;
        //if (fpsCounter >= 1f / fps)
        //{
        //    animationStep++;
        //    if(animationStep == textures.Length)
        //    {
        //        animationStep = 0;
        //    }
        //    lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
        //    fpsCounter = 0f;
        //}
        if (ball.lineLength > 0.68f)
        {
            lineRenderer.material.SetTexture("_MainTex", textures[3]);
        }
        else if(ball.lineLength > 0.5f)
        {
            lineRenderer.material.SetTexture("_MainTex", textures[2]);
        }
        else if(ball.lineLength > 0.25f)
        {
            lineRenderer.material.SetTexture("_MainTex", textures[1]);
        }
        else if (ball.lineLength > 0)
        {
            lineRenderer.material.SetTexture("_MainTex", textures[0]);
        }
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
}
