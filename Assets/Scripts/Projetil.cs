using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour {
	public float StartLifeTime = 10;
	public float LifeTime;
	public float damage = 10;

    public GameObject explosion;
	// Use this for initialization
	void Start () {
		LifeTime = StartLifeTime;

		GetComponent<Rigidbody> ().AddTorque(new Vector3(Random.Range(-10, 10),Random.Range(-10, 10),Random.Range(-10, 10)));
	}
	
	// Update is called once per frame
	void Update () {
		if (LifeTime <= 0)
			Destroy (gameObject);
		
		LifeTime -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("Enemy")) {
			EnemyAI e = other.gameObject.GetComponent<EnemyAI> ();

			if (e) {
				e.TakeDamage (this.damage);
			} else {
				Animals a = other.gameObject.GetComponent<Animals> ();
				a.TakeDamage (this.damage);
			}


		}
        if (!other.gameObject.CompareTag("Cadeirante"))
        {
            Destroy(gameObject);
            Instantiate(explosion,transform.position,Quaternion.identity);
        }

	}
}
