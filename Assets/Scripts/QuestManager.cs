using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour {
	public UIConector uiConector;
	public RectTransform telaFimLevel;
	public List<Quest> penddingQuests;
	public RectTransform questWindow;

    public TextMeshProUGUI MissionGUITitle;
    public TextMeshProUGUI MissionGUIDesc;

	public GameManager gm;
	private Quest questAberta;

	public int numeroDeQuests;
	public int questsCompThisLevel;
	public int questsCompAllLevels = 0;
	public int level;
	public int questPontuation = 1;
	public int pontuation = 0;
	public int maxPenddingQuest = 4;

	public GameObject questObjects;
	//Todas as quests do mundo
	public List<Quest> quests;
	public Transform questMark;
	void Start () {
		questsCompThisLevel = 0;

		penddingQuests = new List<Quest> ();

		quests = new List<Quest> ();
		numeroDeQuests = questObjects.transform.childCount;

		for ( int i=0; i < questObjects.transform.childCount; i++){
			quests.Add(questObjects.transform.GetChild(i).GetComponent<Quest>());
			//Instantiate (questMark, questObjects.transform.GetChild(i).transform);
		}
		//last = 0f;
		uiConector.updatePontuation (0, level);
		startLevel (1);
	}


    [ContextMenu("Proximo Nivel")]
    public void nextLevel(){
		this.questsCompThisLevel = 0;
		this.gm.restartGame (this.level + 1);

		startLevel (this.level + 1);

		this.gm.pauseGame ();
		this.gm.mudarParaTela (this.telaFimLevel);
        telaFimLevel.GetChild(1).GetComponent<Button>().Select();

	}

	public void startLevel(int level){
		//gm.updateLeaderBoard ();

		penddingQuests = new List<Quest> ();

		this.questPontuation = this.questPontuation + 5;
		this.level = level;

		//Debug.Log ("Redefinindo status de todas as missões para 'incompleto'");
		//Percorre todas os objetos das missões e os fazem visiveis e define as missões como incompletas
		for ( int i=0; i < questObjects.transform.childCount; i++){
			GameObject mission = questObjects.transform.GetChild (i).gameObject;
			QuestSetter questInfo = mission.GetComponent<QuestSetter> ();

			questInfo.objetoEntrada.SetActive (true);
			questInfo.objetoEntrada.GetComponent<Quest> ().reset();
		}
	}

	public void aceitarQuest(){
		//Debug.Log ("Clicou em aceitar nova quest");
		if (this.getQuestIndex (questAberta.ID) ==-1) {
			if (this.penddingQuests.Count < this.maxPenddingQuest) {
				//Debug.Log ("Nova quest " + questAberta.title);

				this.penddingQuests.Add (this.questAberta);

				uiConector.addMissionTOGUI (this.questAberta.title, this.questAberta.descricao, this.questAberta.ID);
			} else {
				uiConector.showNotification ("Maximo de missões simultâneas atingido", "Para poder pegar novas missões você precisa concluir as missões atuais", 5);
			}
		}
	}

	public void concluirQuest(int questID){
		int id = getQuestIndex (questID);
		//Debug.Log("ID:" + id);
		penddingQuests[id].complete();
		Quest completed = penddingQuests [getQuestIndex (questID)];
		penddingQuests.Remove (penddingQuests[getQuestIndex(questID)]);


		uiConector.removeMissionFromGUI(questID);

		questsCompAllLevels++;
		questsCompThisLevel++;
		this.pontuation += this.questPontuation;

		this.uiConector.updatePontuation (this.pontuation, level);
		//Debug.Log ("Pontuacao ate o momento:" + this.pontuation);

		uiConector.missionCompleted (completed, questPontuation);

		if (this.questsCompThisLevel == this.numeroDeQuests) {
			nextLevel ();
		}

		gm.updateScore (this.pontuation);
		gm.updateLeaderBoard ();
	}

	//Pega o indice de uma quest na lista de quest dado o seu ID
	private int getQuestIndex(int questID){
		foreach (Quest q in penddingQuests) {
			if(q.ID == questID ){
				return penddingQuests.IndexOf(q);
			}
		}
		return -1;
	}

	public void exibirQuest(Quest q){
		Time.timeScale = 0;
		this.questAberta = q;

		MissionGUITitle.text= questAberta.title;
        MissionGUIDesc.text = questAberta.descricao;


        questWindow.GetChild(1).GetChild(0).GetComponent<Button>().Select();

        gm.mudarParaTela (questWindow);
	}

	public void reset(){
		this.pontuation = 0;
		this.level = 0;
		questsCompThisLevel = 0;
	}
		
}
