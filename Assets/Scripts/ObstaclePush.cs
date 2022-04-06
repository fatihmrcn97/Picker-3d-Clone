using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;

    [SerializeField] private GameObject pusher;
     


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null)
        {
            Debug.Log("icerdemi");
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FirstStage"))
        {
            // Push all the collected items
            pusher.transform.DOLocalMove(new Vector3(pusher.transform.localPosition.x, pusher.transform.localPosition.y,-3.08f),1.5f,false);
            StartCoroutine(Elevator());
        }
       
    }
   





    IEnumerator Elevator()
    {
        yield return new WaitForSeconds(3f);
        pusher.transform.localPosition = new Vector3(0, 0.297f, 3.234f);
         
    }


}
