using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondScript : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody rb;
    //public Transform ballTransform;
    public float teleportDistance = 2.0f;
    Vector3 contactPoint;
    Vector3 teleportPoint;
    public LayerMask teleportLayerMask; // teleport noktas� i�in zemin layerini se�mek
    public GameObject teleportDestination; // teleport destination diye bir empty gameobjectimiz var


    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = ball.GetComponent<Rigidbody>();
    }

    //IEnumerator waiter()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    ball.transform.position = new Vector3(teleportPoint.x, gameObject.transform.position.y + 1.26f, teleportPoint.z);
    //    rb.velocity = new Vector3(0, 0, 0);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // teleport destinasyon objesinin collider boundlar�
            Bounds destinationBounds = teleportDestination.GetComponent<Collider>().bounds;

            // destinasyon objesi ile duvar trigger noktas� aras� en yak�n nokta
            Vector3 closestPoint = destinationBounds.ClosestPoint(other.gameObject.transform.position);

            // teleport y�n�n� duvar'dan closestpoint'e do�ru �evirme
            Vector3 teleportDirection = transform.position - closestPoint;
            teleportDirection.y = 0; // dikey y�n� alm�yoruz, y sini sonra ayarl�caz zemine g�re
            teleportDirection.Normalize();

            // destinasyon objesinde sectigimiz closest noktaya g�re teleport noktas� belirleme
            Vector3 newPosition = closestPoint + teleportDirection * teleportDistance;

            // toptan a�a�� ray at�yoruz zemini bulmak i�in
            Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.up); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                newPosition.y = hit.point.y + 0.2f; // teleport noktas�n�n y'sini zemin y'sine g�re ayarl�yoruz (+0.2f zeminin i�ine ���nlanmas�n diye)
            }

            rb.velocity = Vector3.zero; // harekete devam etmesin diye h�z� s�f�rl�yoruz
            other.transform.position = newPosition; // topu teleport ediyoruz o noktaya
        }
    }







    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        contactPoint = other.ClosestPointOnBounds(transform.position);
    //        teleportPoint = CalculateTeleportPoint(contactPoint, transform.position, transform.localScale.x);
    //        Debug.Log("Contact Point: " + contactPoint);

    //        StartCoroutine(waiter());
    //    }
    //}

    //private Vector3 CalculateTeleportPoint(Vector3 contactPoint, Vector3 sphereCenter, float sphereRadius)
    //{
    //    Vector3 direction = contactPoint - sphereCenter;
    //    direction.y = 0f; // Ignore vertical component

    //    Vector3 normalizedDirection = direction.normalized;

    //    Vector3 teleportPoint = contactPoint + normalizedDirection * sphereRadius;

    //    return teleportPoint;
    //}
}
