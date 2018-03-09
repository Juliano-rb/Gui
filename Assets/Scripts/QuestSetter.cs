using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Esta classe guarda informações no geral sobre a missão, guarda onde o usuario pegará a missão e onde ela será concluída, e atribui os dados necessários aos objetos
 * */
public class QuestSetter : MonoBehaviour {
	public string titleQuest;
	public string descriptionQuest;
	public int IDQuest;

	public GameObject objetoEntrada; 
	public GameObject objetoFim; 
	// Use this for initialization
	void Start () {

		Debug.Log ("Configurando objetos referentes à missão " + IDQuest);

		objetoEntrada.AddComponent<Quest>();

		objetoEntrada.GetComponent<Quest> ().setInfo (IDQuest, titleQuest, descriptionQuest);

		objetoFim.AddComponent<QuestCatch> ();
		objetoFim.GetComponent<QuestCatch> ().setID (IDQuest);

		this.objetoEntrada.tag = "questEntry";
		this.objetoFim.tag = "questEnd";
	}
}
