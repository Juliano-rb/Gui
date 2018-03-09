using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIConector : MonoBehaviour {
	//Game objects
	public Text pontuacao;
	public Slider volumeSlider;
	public AudioManager audioManager;

	public RectTransform notifiPopup;
	public Text LevelCompletText;
	public RectTransform missionSlots;
	public RectTransform leaderBoard;
	//Classes
	//public QuestManager questm;
	// Use this for initialization
	void Start () {
		//this.volumeSlider = 
	}
	
	public void updateVolume(){
		audioManager.setVolume (volumeSlider.value);
	}

	public void updatePontuation(int pontuacaoAtual, int faseAtual){
		this.pontuacao.text = pontuacaoAtual.ToString (); //questm.pontuation.ToString();
		this.LevelCompletText.text = "Fase <b>" + faseAtual.ToString() + "</b> concluída!";
	}

	public void missionCompleted(Quest quest, float recompensa){
		string description = "Nome: " + quest.title + ".\n" + "Recompensa: " + recompensa + " pontos.";
		this.showNotification ("MISSÃO CONCLUÍDA", description, 5f);
	}

	public void showNotification(string title, string msg, float time){
		Notificacao notify = this.notifiPopup.GetComponent<Notificacao>();

		notify.show (title, msg, time);
	}

	public void addMissionTOGUI(string titleMission, string descMission, int IDMission){
		Debug.Log ("Adicionando missao " + titleMission + " à GUI");
		Transform slot = missionSlots.GetChild( getFirstEmptyMissionSlot());
		slot.gameObject.SetActive (true);

		slot.GetChild (0).GetComponent<Text> ().text = titleMission;
		slot.GetChild (1).GetComponent<Text> ().text = descMission;
		slot.GetChild (2).GetComponent<Text> ().text = IDMission.ToString();

		Debug.Log ("Missao adicionadoa à GUI com sucesso");
	}

	public void removeMissionFromGUI(int ID){
		
		//acha o slot que contém a missão que deve-se ser removida
		int i=0;
		Transform slot;
		string missionID;
		do {
			slot = missionSlots.GetChild (i);
			missionID = slot.GetChild(2).GetComponent<Text>().text;
			i++;
		} while( !missionID.Equals( ID.ToString() )  && i < slot.childCount);

		slot.gameObject.SetActive (false);
	}
	//
	//Percorre os espaços reservados para as missões em andamentos na interface procurando um espaço vazio
	//
	private int getFirstEmptyMissionSlot(){
		Debug.Log ("Procutando o primeiro slot vazio para inserir a nova missao");
		int i=0;
		Transform slot;

		while(missionSlots.GetChild (i).gameObject.activeSelf  && i < missionSlots.childCount){
			Debug.Log("Verificando slot:" + i);
			i++;
		}
		
		return i;
	}

	public void updateLeaderBoardPanel(List<dreamloLeaderBoard.Score> scores){
		Debug.Log ("Placares: " + leaderBoard.childCount);
		Debug.Log(scores[1].playerName);
		for (int i = 0; i < leaderBoard.childCount && i < scores.Count; i++) {
			Debug.Log ("Exibindo pontuacao de " + scores [i].playerName);
			leaderBoard.GetChild (i).Find ("Nome").GetComponent<Text>().text = scores[i].playerName.ToString();
			leaderBoard.GetChild (i).Find ("Placar").GetComponent<Text>().text = scores[i].score.ToString();
		}

	}
}
