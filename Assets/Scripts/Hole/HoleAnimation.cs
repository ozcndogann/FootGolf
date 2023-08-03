using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.UIElements;

public class HoleAnimation : MonoBehaviour
{
    [SerializeField] private GameObject holePos;

    public float MaxHoleDropOffset;

    private float stayTimer = 0;
    public float MaxStayTime;
    private bool hasDropped = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.enabled && other.CompareTag("Ball"))
        {
            stayTimer += Time.deltaTime;
            Ball putter = other.GetComponent<Ball>();


            Vector3 ballXYpos = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            Vector3 holeXYpos = new Vector3(transform.position.x, 0f, transform.position.z);

            if (Mathf.Abs(ballXYpos.x - holeXYpos.x) < MaxHoleDropOffset &&
                Mathf.Abs(ballXYpos.y - holeXYpos.y) < MaxHoleDropOffset &&
                (other.attachedRigidbody.velocity.magnitude < 1 || stayTimer >= MaxStayTime))
            {
                if (!hasDropped)
                {
                    other.transform.position = holePos.transform.position + new Vector3(0,0.4f,0);
                    other.attachedRigidbody.velocity = Vector3.zero;
                    hasDropped = true;
                }
            }
        }
    }

}
