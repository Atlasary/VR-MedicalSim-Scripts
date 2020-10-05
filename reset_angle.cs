using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset_angle : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,0)) ;
	}
}
