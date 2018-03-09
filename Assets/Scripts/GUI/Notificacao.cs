using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Notificacao : MonoBehaviour {
	public Text titulo;
	public Text corpo;

	public float totalTime;
	public float tempoRestante;

	public bool started;
	// Use this for initialization
	void Start () {
		started = false;
		totalTime = 10f;
		tempoRestante = 10f;

		titulo = transform.Find ("title").GetComponent<Text>();
		corpo = transform.Find ("body").GetComponent<Text>();

		gameObject.SetActive (false);

		show ("Bem vindo", "Aqui você passará por grandes aventuras, caro aluno.", 5);
	}
	public void show(string title, string desc, float totalTime){
		this.titulo.text = title;
		this.corpo.text = desc;
		this.totalTime = totalTime;
		this.tempoRestante = totalTime;

		gameObject.SetActive (true);

		started = true;
	}
		
	// Update is called once per frame
	void Update () {
		if (started) {
			if (this.tempoRestante > 0f)
				this.tempoRestante -= Time.deltaTime;
			else {
				this.gameObject.SetActive (false);
				started = false;
			}
		}
	}
}
