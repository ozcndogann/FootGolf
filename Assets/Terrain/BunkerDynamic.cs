using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BunkerDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;
    //public GameObject particles;
    //public static bool particleEnd = false;
    //public static bool bunkerTouch = false;
    //public GameObject Bunker;
    //public BoxCollider box;
    //private bool inBunker = false;
    //public float sandHeightThreshold;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Ball").GetComponent<PhotonView>();
        //rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;
        //box = gameObject.GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (view.IsMine)
            {
                other.gameObject.GetComponent<Rigidbody>().mass = 1.75f;
                other.gameObject.GetComponent<Rigidbody>().angularDrag = 0.05f;
                other.gameObject.GetComponent<Rigidbody>().drag = 0.075f;
                other.gameObject.GetComponent<SphereCollider>().material.bounciness = 0.21f;
                //rb.mass = 1.75f;
                //rb.angularDrag = 0.05f;
                //rb.drag = 0.075f;
                //rbphy.bounciness = 0.21f;
            }
        }
    }

    //private void Update()
    //{
    //    if (rb.transform.position.y < sandHeightThreshold)
    //    {
    //        inBunker = true;
    //        ApplyBunkerPhysics();
    //    }
    //    else
    //    {
    //        inBunker = false;
    //    }

    //    if (rb.velocity.magnitude == 0)
    //    {
    //        particleEnd = true;
    //    }
    //    else
    //    {
    //        particleEnd = false;
    //    }

    //    if (bunkerTouch == true)
    //    {
    //        rb.mass = 1.65f;
    //        rb.angularDrag = 0.05f;
    //        rb.drag = 0.075f;
    //    }
    //    else
    //    {
    //        rb.mass = 1;
    //        rb.angularDrag = 0.05f;
    //        rb.drag = 0.075f;
    //    }

    //}

    //void ApplyBunkerPhysics()
    //{
    //    rb.mass = 1.8f;
    //    rb.angularDrag = 0.03f;
    //    rb.drag = 0.05f;
    //    rbphy.bounciness = 0.1f;
    //}


    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Ball"))
    //    {
    //        bunkerTouch = true;

    //        if (!particleEnd)
    //        {
    //            Instantiate(particles, rb.transform.position, Quaternion.identity);
    //        }
    //    }
    //    else
    //    {
    //        bunkerTouch = false;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        bunkerTouch = false;

    //        //box.enabled = true;
    //    }
    //}

}