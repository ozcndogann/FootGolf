using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplineCam : MonoBehaviour
{
    [SerializeField] SplineFollower camSpline;
    public float journeyTime;

    void Start()
    {
        StartCoroutine(MoveCameraWithSpline());
    }
    private IEnumerator MoveCameraWithSpline()
    {

        camSpline.follow = true;
        yield return new WaitForSeconds(journeyTime);
        camSpline.follow = false;
    }
}
