using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCatch : MonoBehaviour {
	public int questID;


	public void setID(int id){
		this.questID = id;
	}
	/*void Start(){
		
	}*/
	/*void OnTriggerEnter (Collider other){
		GameObject go = other.gameObject;
		if(other.gameObject.CompareTag("Cadeirante")){
			Debug.Log ("Concluiu missao " + questID);
			qm.concluirQuest (questID);
			Destroy(this);
		}
	}

	void OnCollisionEnter (Collision other){
		GameObject go = other.gameObject;
		if(other.gameObject.CompareTag("Cadeirante")){
			
		}
	}*/
}
