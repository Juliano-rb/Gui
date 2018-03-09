using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingObject : MonoBehaviour {

	public float rotateSpeed = 1;
	public Vector3 rotateVector = new Vector3 (1, 0, 0);
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotateVector*Time.deltaTime*rotateSpeed);
	}
}
