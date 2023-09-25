using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform hole;
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 45f;
    [SerializeField] private float waitDuration = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ball").transform;
        hole = GameObject.FindGameObjectWithTag("Hole").transform;

        cam.transform.position = hole.position + new Vector3(0, 5, 0);

        StartCoroutine(MoveCameraCoroutine());
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

            cam.transform.position = Vector3.Lerp(hole.position + new Vector3(0, 5, 0), target.position + new Vector3(0, 1, 0), journeyFraction);
            yield return null;
        }


        cam.transform.position = target.position;
    }
}