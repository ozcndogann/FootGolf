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
    public LayerMask teleportLayerMask; // teleport noktasý için zemin layerini seçmek
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
            // teleport destinasyon objesinin collider boundlarý
            Bounds destinationBounds = teleportDestination.GetComponent<Collider>().bounds;

            // destinasyon objesi ile duvar trigger noktasý arasý en yakýn nokta
            Vector3 closestPoint = destinationBounds.ClosestPoint(other.gameObject.transform.position);

            // teleport yönünü duvar'dan closestpoint'e doðru çevirme
            Vector3 teleportDirection = transform.position - closestPoint;
            teleportDirection.y = 0; // dikey yönü almýyoruz, y sini sonra ayarlýcaz zemine göre
            teleportDirection.Normalize();

            // destinasyon objesinde sectigimiz closest noktaya göre teleport noktasý belirleme
            Vector3 newPosition = closestPoint + teleportDirection * teleportDistance;

            // toptan aþaðý ray atýyoruz zemini bulmak için
            Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.up); // bi týk altýndan baþlatýyoruz topun kendisini algýlamasýn diye
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                newPosition.y = hit.point.y + 0.2f; // teleport noktasýnýn y'sini zemin y'sine göre ayarlýyoruz (+0.2f zeminin içine ýþýnlanmasýn diye)
            }

            rb.velocity = Vector3.zero; // harekete devam etmesin diye hýzý sýfýrlýyoruz
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
