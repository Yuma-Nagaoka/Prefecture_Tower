using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kumo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += new Vector3 (-0.005f, 0f, 0f);
		if (transform.position.x < -7.8f) {
			transform.position = new Vector3 (7.87f, 6.8f, 0f);
		}
	}
}
