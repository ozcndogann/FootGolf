using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RainyDynamic : MonoBehaviour
{
    private Rigidbody rb;
    //public GameObject particles;
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rb.mass = 1.1f;
        rb.angularDrag = 0.33f;
        rb.drag = 0.5f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            rb.mass = 1.1f;
            rb.angularDrag = 0.33f;
            rb.drag = 0.5f;
        }
    }
}
