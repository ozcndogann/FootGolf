using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform hole;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed =50f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float waitDuration = 5f;
    [SerializeField] private float yVal;
    public static bool TimeS;


    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
        hole = GameObject.FindGameObjectWithTag("Hole").transform;

        cam.transform.position = hole.position + new Vector3(0, 5, 0);

        //TimeS = false;
        StartCoroutine(SequenceCoroutine());
    }

    private IEnumerator SequenceCoroutine()
    {
        yield return StartCoroutine(ShowCameraCoroutine());

        yield return StartCoroutine(MoveCameraCoroutine());
    }


    private IEnumerator ShowCameraCoroutine()
    {
        float journeyLength = Vector3.Distance(hole.position, target.position);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / moveSpeed)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float journeyFraction = distanceCovered / journeyLength;

            cam.transform.position = Vector3.Lerp(hole.position + new Vector3(0, 5, 0), hole.position + new Vector3(60, 30, 60), journeyFraction);

            cam.transform.LookAt(hole.position);

            yield return null;
        }


        cam.transform.position = hole.position + new Vector3(50, 30, 50);
    }

    private IEnumerator MoveCameraCoroutine()
    {
        //yield return new WaitForSeconds(0.5f);

        float journeyLength = Vector3.Distance(hole.position, target.position);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / moveSpeed)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float journeyFraction = distanceCovered / journeyLength;

            cam.transform.position = Vector3.Lerp(hole.position + new Vector3(60, 30, 60), target.position + new Vector3(0, 1, 0), journeyFraction);

            cam.transform.LookAt(hole.position);


            yield return null;
        }

        cam.transform.rotation = Quaternion.Euler(5.76f,yVal , 0);
        //TimeS = true;
        //cam.transform.position = target.position;
    }
}