using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RainyDynamic : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicMaterial rbphy;
    private PhotonView view;
    //public GameObject particles;
    void Start()
    {
        view = GameObject.FindGameObjectWithTag("Ball").GetComponent<PhotonView>();
        rb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        rbphy = GameObject.FindGameObjectWithTag("Ball").GetComponent<SphereCollider>().material;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (view.IsMine)
            {
                rb.mass = 1.035f;
                rb.angularDrag = 0.05f;
                rb.drag = 0.075f;
                rbphy.bounciness = 0.5f;
            }
        }
    }
}
