using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawPoint : MonoBehaviour {

	public GameObject prefab;

	//public int count = 0;
	public List<GameObject> childrens;
	public GameManager gm;
	public int maxChildren = 2;
	public float viewField = 15f;
	public float speed=2f;
    public bool isProfessor;
	//public int spawnTime = 5;

	void Start(){
		childrens = new List<GameObject> ();

		//Fica chamando em loop a função de spawnar inimigos com um intervalo de tempo definido em spawnTime
		//InvokeRepeating ("Spawn", spawnTime, spawnTime);

		SpawnAll ();
	}
	void SpawnAll(){
		for (int i = 0; i < maxChildren; i++) {
			Spawn ();
		}
	}
	void Spawn()
	{
		Debug.Log (childrens.Count);

		if (childrens.Count >= maxChildren) {
			return;
		}
        
		//Instancia um inimigo dado o seu prefab e a posição e rotação definida pelo local que o ponto de spawn está
		GameObject e = Instantiate(prefab, transform.position, transform.rotation);
		//e.GetComponent<EnemyAI> ().target = gm.Player.transform;
		//Configura as propriedades de alvo do inimigo e outros
		e.GetComponent<EnemyAI> ().personagem = gm.Player;
		e.GetComponent<EnemyAI> ().father = this;
		e.GetComponent<EnemyAI> ().viewField = viewField;
		e.GetComponent<EnemyAI> ().speed = speed;
        if(isProfessor)
		    e.GetComponent<EnemyAI> ().professor = true;
		//Adiciona o inimigo criado à lista de inimigos criados por este ponto de spawn, para q seja possível controla-lo depois,
		//(por exemplo remove-lo e controlar a quantidade de inimigos
		childrens.Add (e);
	}

	public void removeReference(GameObject g){
		childrens.Remove (g);
	}

	public void aumentarDificuldade(int multip){
		this.maxChildren += multip;

		this.viewField *= 1.2f*multip;
		//if(maxChildren > 4)
			this.speed *= 1.1f*multip;
	}
	public void reset(){
		foreach (GameObject c in childrens) {
			Destroy (c);
		}
		childrens.Clear ();

		SpawnAll ();
	}
}
