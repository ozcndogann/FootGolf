using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunkers : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("bunker"))
        {
            Destroy(this.gameObject, 4);
        }
        if (collision.gameObject.CompareTag("lake"))
        {
            Destroy(this.gameObject, 4);
        }
    }


}
