using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour {
	//Se ativo, o personagem fica falando todas as suas falas em loop, usado nos zumbis :)
	public bool loopSpeech = true;
	//Dialogo ativo
	public bool falando = true;
	//Objeto que contém a mensagem
	public Transform DialogBox;
	//Texto da mensagem que será alterado
	public TextMesh mensagemGB;
	private Transform direction;
	//Tempo que uma mensagem ficará sendo exibida
	public float msgDelay = 10000;
	//Array de falas
	public string[] falas;
	//Contador de tempo da msg atual
	private float msgTime = 0;
	//Indice da mensagem atual(no array)
	private int actMsg = -1;

	// Use this for initialization
	void Start(){
		proximaFala ();


	}
	
	// Update is called once per frame
	void Update () {
		//Mantém a caixa de dialogo no alto e olhando para o jogador
		//DialogBox.position.Set (transform.position.x, transform.position.y+100, transform.position.z);
		DialogBox.transform.LookAt (direction);

		if (falando) {
			msgTime += Time.deltaTime;
			if (msgTime >= msgDelay) {
				if (loopSpeech)
					proximaFala ();
				else {
					proximaFala ();
					setFalar (false);
				}
			}
		}

	}

	public void setFalar(bool falar){
		if (falar) {
			this.DialogBox.gameObject.SetActive (true);
			falando = true;
		} else if(!falar){
			this.DialogBox.gameObject.SetActive (false);
			falando = false;
		}

	}
	void proximaFala(){
		actMsg++;

		//Leng
		if (actMsg >= falas.Length)
			actMsg = 0;

		/*Debug.Log (falas [0]);
		Debug.Log ("Proxima fala: " + actMsg);
		Debug.Log(" Fala atual: " + falas [actMsg] + " tamanho do array de falas: " + this.falas.Length);*/

		falas[actMsg] = falas[actMsg].Replace("//n","\n");

		this.mensagemGB.text = falas[actMsg];
		msgTime = 0;
	}
}
