using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Tracking.Follow;

public class PunchHand : MonoBehaviour
{
    public ObjectFollower hand;

    public float forceScale = 3.0f;

    private Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rBody.MovePosition(hand.transform.position);
        rBody.MoveRotation(hand.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Hit!");
        
        Rigidbody otherBody = other.gameObject.GetComponentInChildren<Rigidbody>();
        if (otherBody != null) return;

        Vector3 avgPoint = Vector3.zero;       
        foreach(var p in other.contacts)
        {
            avgPoint += p.point; 
        }
        avgPoint /= other.contacts.Length;

        Vector3 dir = (avgPoint - transform.position).normalized;
        otherBody.AddForceAtPosition(dir * forceScale * rBody.velocity.magnitude, avgPoint);

    }
}
