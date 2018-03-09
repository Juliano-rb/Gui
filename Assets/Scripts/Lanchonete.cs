using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanchonete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other)
	{
		GameObject gb = other.gameObject;
		Debug.Log("Colidiu Cantina");
		if (gb.CompareTag("Cadeirante"))
		{
			float health = gb.GetComponent<PlayerHealth> ().currentHealth;
			gb.GetComponent<CharacController> ().energia = 100;
			if (health < 100)
				gb.GetComponent<PlayerHealth> ().currentHealth += 50;
			else
				gb.GetComponent<PlayerHealth> ().currentHealth = 100;
		}

	}
}
