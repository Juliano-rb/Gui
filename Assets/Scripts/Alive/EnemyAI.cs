using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
public class EnemyAI : MonoBehaviour {
	public GameObject personagem;

    //public Transform target;
	//Estes valores também são setados assim que o objeto é instanciado, portanto os valores padrão não são necessariamente estes,
	public float speed = 1;
    public float viewField = 5f;
	public float Life = 50;

    public float sleeping = 0;

	public string status = "idle";
	GameManager manager;
	PlayerHealth playerHealth;
	public SpawPoint father;
	public bool professor;
	void start(){
		this.Life = 50;
		GetComponent<Speech> ().setFalar (false);

	}

	// Update is called once per frame
	void Update () {
        //Calcula a distancia do inimigo ate o personagem
		float dist = Vector3.Distance(personagem.transform.position, transform.position);
		float alunoReclamando = personagem.GetComponent<CharacController> ().alunoReclamao;
		bool changeDialog = false;
		//print ("Distancia = " + dist + "status: " + status + " " + viewField);

		//Se o personagem esta fora do campo de visão, se move normalmente para uma direção aleatoria
		if (dist > viewField) {
			status = "idle";

			//Debug.Log("Constraint:s" + GetComponent<Rigidbody> ().constraints);

			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		} else {
			
			float raioGrito = personagem.GetComponent<CharacController> ().raioGrito;
			if (dist > raioGrito || alunoReclamando <= 0){
				if (!status.Equals ("stalker")) {
					if(professor)
						changeDialog = true;
					status = "stalker";
					GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
				}
			}
			else
				if (!status.Equals ("sleep") && professor) {
					changeDialog = true;
					status = "sleep";
				GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
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
			if(changeDialog)
				GetComponent<Speech> ().setFalar (true);
			transform.LookAt (personagem.transform);

			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			break;
		case "sleep":
			if(changeDialog)
				GetComponent<Speech> ().setFalar (false);
			
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
			//Debug.Log ("Colidiu com o personagem");
			playerHealth = other.gameObject.GetComponent<PlayerHealth> ();
			playerHealth.TakeDamage (10, transform.forward);
		}

	}


	public void TakeDamage(float damage){
		if(Life > 0)
			Life -= damage;

		if (Life <= 0) {
			sleeping = 60;
			if(father)
				father.removeReference (this.gameObject);
			Destroy (this);
			Destroy (gameObject);
		}
	}
}
