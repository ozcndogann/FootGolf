using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            rb.mass = 1f;
            rb.angularDrag = 0.05f;
            rb.drag = 0.075f;
            rbphy.bounciness = 0.7f;
        }
    }
}
