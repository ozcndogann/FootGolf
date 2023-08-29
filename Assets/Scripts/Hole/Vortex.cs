using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    private Collider vortexCollider;

    public float VortexForce;

    private void Awake()
    {
        vortexCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 normal = other.transform.position - vortexCollider.bounds.center;
            normal.y = Mathf.Abs(normal.y);
            other.attachedRigidbody.AddForce(0,-normal.y * VortexForce,0);
            other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
