using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerDynamic : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            rb.mass = 1.45f;
            rb.angularDrag = 0.0725f;
            rb.drag = 0.10875f;
            GameObject Particle = particles;
            Instantiate(Particle, new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z), Quaternion.identity);
            Destroy(Particle, 0.1f);
        }
    }
}