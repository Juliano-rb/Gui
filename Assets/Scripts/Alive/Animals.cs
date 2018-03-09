using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Animals : MonoBehaviour {
	public GameObject personagem;

	//public Transform target;
	public float speed;
	public float viewField = 10;
	public float sleeping = 0;

	public float Life = 50;
	public string status = "idle";
	GameManager manager;
	PlayerHealth playerHealth;

	void start(){
		this.Life = 50;
	}

	// Update is called once per frame
	void Update () {
		//Calcula a distancia do inimigo ate o personagem
		float dist = Vector3.Distance(personagem.transform.position, transform.position);
		//float alunoReclamando = personagem.GetComponent<CharacController> ().alunoReclamao;
		//bool changeDialog = false;
		//Se o personagem esta fora do campo de visão, se move normalmente para uma direção aleatoria
		if (dist > viewField) {
			status = "idle";

		} else {
			if (!status.Equals ("stalker")) {
				status = "stalker";
			}
			else
				if (!status.Equals ("sleep")) {
					status = "sleep";
				}

		} 
			
		switch (status) {
		case "idle":
			float directionY = Random.Range (-1.0f, 1.0f);

			Vector3 rotate = new Vector3 (0, directionY * speed, 0);
			rotate.Normalize ();
			transform.Rotate (rotate);

			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			break;
		case "stalker":
			speed = 5;
			transform.LookAt (personagem.transform);

			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			break;
		case "sleep":
			if (sleeping > 0)
				sleeping -= Time.deltaTime;
			break;
		}
	}



	void OnCollisionEnter(Collision other)
	{
		// If the entering collider is the player...
		if(other.gameObject.transform == this.personagem.transform)
		{
			Debug.Log ("Colidiu com o personagem");
			playerHealth = other.gameObject.GetComponent<PlayerHealth> ();
			playerHealth.TakeDamage (10, transform.forward);
		}

	}


	public void TakeDamage(float damage){
		if(Life > 0)
			Life -= damage;

		if (Life <= 0) {
			sleeping = 60;
		}
	}
}
