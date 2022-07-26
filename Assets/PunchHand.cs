using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Tilia.Output.InteractorHaptics;
using Tilia.SDK.SteamVR.Haptics;
using UnityEngine;
using Valve.VR;
using Zinnia.Tracking.Follow;

public class PunchHand : MonoBehaviour
{
    public GameObject hand;
    public SteamVRActionHapticPulser handPulser;

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
        Debug.Log("Hit!");

        Rigidbody otherBody = other.gameObject.GetComponentInChildren<Rigidbody>();
        if (otherBody == null) return;        

        Vector3 avgPoint = Vector3.zero;
        foreach (var p in other.contacts)
        {
            avgPoint += p.point;
        }
        avgPoint /= other.contacts.Length;

        Vector3 dir = (avgPoint - transform.position).normalized;
        otherBody.AddForceAtPosition(dir * forceScale * rBody.velocity.magnitude, avgPoint);

        handPulser.VibrationAction.Execute(0, handPulser.Duration, handPulser.Frequency, handPulser.Intensity, handPulser.Controller);
    }

    //IEnumerator LongVibration(float length, float strength)
    //{
    //    for (float i = 0; i < length; i += Time.deltaTime)
    //    {
    //        Valve.                
    //        SteamVR_Controller.Input[(int)handSteam.index].TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
    //        yield return null;
    //    }
    //}

    //IEnumerator LongVibration(int vibrationCount, float vibrationLength, float gapLength, float strength)
    //{
    //    strength = Mathf.Clamp01(strength);
    //    for (int i = 0; i < vibrationCount; i++)
    //    {
    //        if (i != 0) yield return new WaitForSeconds(gapLength);
    //        yield return StartCoroutine(LongVibration(vibrationLength, strength));
    //    }
    //}
}
