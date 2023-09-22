using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private Rigidbody ballRigidbody;
    public float teleportDistance = 2.0f; // teleport noktas�n�n duvardan uzakl���n� belirliyoruz (hala i�e yaramas�n� sa�layamad�m) 
    public LayerMask teleportLayerMask; // teleport noktas� i�in zemin layerini se�mek
    public GameObject teleportDestination; // teleport destination diye bir empty gameobjectimiz var

    private void Start()
    {
        ballRigidbody = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

   

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
            Ray ray = new Ray(newPosition + Vector3.up * 10, Vector3.down); // bi t�k alt�ndan ba�lat�yoruz topun kendisini alg�lamas�n diye
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, teleportLayerMask))
            {
                newPosition.y = hit.point.y + 0.2f; // teleport noktas�n�n y'sini zemin y'sine g�re ayarl�yoruz (+0.2f zeminin i�ine ���nlanmas�n diye)
            }

            IEnumerator WFSOOB()
            {
                yield return new WaitForSeconds(1.95f);

                ballRigidbody.velocity = Vector3.zero; // harekete devam etmesin diye h�z� s�f�rl�yoruz
                other.transform.position = newPosition; // topu teleport ediyoruz o noktaya
            }

            StartCoroutine(WFSOOB());

        }
    }
}
