using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {
    public Rigidbody rBody;
	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "goal")
        {
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
            transform.position = Vector3.up*1.5f;
        }
    }
}
