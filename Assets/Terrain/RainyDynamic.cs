using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RainyDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;

    //public GameObject particles;
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;

        rb.mass = 1;
        rb.angularDrag = 0.05f;
        rb.drag = 0.075f;
        rbphy.bounciness = 0.5f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            rb.mass = 1.1f;
            rb.angularDrag = 0.05f;
            rb.drag = 0.075f;
            rbphy.bounciness = 0.4f;
        }
    }
}
