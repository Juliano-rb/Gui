using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject Player;
    public RectTransform viewFalhaAcessibilidade;
	public RectTransform viewTelaInicial;
	public RectTransform viewGameOver;
    public RectTransform viewMissions;

    public RectTransform telaAtual = null;

    public Transform[] hiPolyObjects;
    public Toggle menuToggle;

	//Textos da janela de falha de acessibilidade
    public Text TitleField;
    public Text DescField;
	//Objeto de entrada para o Nome do jogador
	public RectTransform playerNameField;

	public Transform startPosition;
	//Barras de status
	public Slider healthSlider; 
	public Slider forcaSlider;

	public Transform spawPointsGroup;
	public List<SpawPoint> spawPointsScripts;

	public UIConector uiConector;
	public string playerName;
	public dreamloLeaderBoard leaderBoard;
	public List<dreamloLeaderBoard.Score> publicScores;
	public QuestManager qm;
	public int dificuldade;

	void Start(){
		//Pausa o Jogo
		Time.timeScale = 0;
		//Debug.Log ("Carregando pontos de spaw de inimigos...");

		for (int i = 0; i < spawPointsGroup.childCount; i++) {
			spawPointsScripts.Add( spawPointsGroup.GetChild(i).gameObject.GetComponent<SpawPoint>() );
		}

		leaderBoard = GetComponent<dreamloLeaderBoard> ();

		StartCoroutine ("updateLeaderBoard");
	}

    
    private void Update()
    {
        if (Input.GetButton("Cancel") && Time.deltaTime != 0)
        {
            pauseGame();
        }

        
        if (Input.GetButtonDown("MiniWindow") && Time.deltaTime != 0)
        {
            showMissions();
        }


    }

    public void selectGUIItem(Selectable item)
    {
        item.Select();
    }

    public void showMissions()
    {
        viewMissions.gameObject.SetActive(!viewMissions.gameObject.activeSelf);
 
    }

    public void ShowFalha(Falha f)
    {
		//Debug.Log("Exibindo tela de falha");

		mudarParaTela (viewFalhaAcessibilidade);
        TitleField.text = f.Title;
        DescField.text = f.Desciption;// "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum quis purus vitae quam tempor blandit vel nec orci. Fusce euismod turpis vel orci facilisis, eget bibendum lectus congue.";
        Time.timeScale = 0;
    }
	public void showGameOver(){

        viewGameOver.GetChild(1).GetChild(0).GetComponent<Button>().Select();
        mudarParaTela (viewGameOver);
	}

	public void mudarParaTela(RectTransform tela){
		if (tela) {
			this.viewTelaInicial.parent.gameObject.SetActive (true);

			if (this.telaAtual)
				this.telaAtual.gameObject.SetActive (false);
			this.telaAtual = tela;
			this.telaAtual.gameObject.SetActive (true);

        } else {
			startGame ();
		}
	}
	public void updateLeaderBoard(){
        //Debug.Log ("Carregando Scores");
		leaderBoard.LoadScores ();
		//leaderBoard.ToListHighToLow();

	}
	public int updateScore(int score){
		//Debug.Log ("Atualizando pontuação no hanking");
	
		leaderBoard.AddScore (this.playerName, score);

		return 1;
	}
    public void changeScene(int level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

	public void startGame(){
		this.dificuldade = 0;
		this.playerName = this.playerNameField.GetComponent<InputField>().text;
		playerName = playerName.Replace ("/", "_");
		playerName = playerName.Replace ("*", "_");
		playerName = playerName.Replace ("&", "_");
		playerName = playerName.Replace ("%", "_");
		if (playerName.Length >= 3) {
			
			playerNameField.GetComponent<Image> ().color = new Color (116f/255f, 121f/255f, 225f/255f, 119f/255);

			this.viewTelaInicial.gameObject.SetActive (false);
			this.viewTelaInicial.parent.gameObject.SetActive (false);
			//this.paused = false;

			Time.timeScale = 1;
		} else {
			playerNameField.GetComponent<Image> ().color = new Color (225f/255, 116f/255, 116f/255, 119f/255);
		}
	}

	public void pauseGame(){
		//this.paused = true;
		Time.timeScale = 0;
		mudarParaTela(viewTelaInicial);
	}

	public void updateForca(float valor){
		this.forcaSlider.value = valor;
	}
	public void updateLife(float valor){
		this.healthSlider.value = valor;
	}
	public void gameOverRestart(){
		Application.LoadLevelAsync(Application.loadedLevel);

		/*this.dificuldade = 0;
		qm.pontuation = 0;
		restartGame (this.dificuldade);*/
	}

	public void restartGame(int dificuldade){
		this.dificuldade = dificuldade;
		Player.transform.SetPositionAndRotation (this.startPosition.position, Player.transform.rotation);
		PlayerHealth ph = Player.gameObject.GetComponent<PlayerHealth> ();
		CharacController cc = Player.gameObject.GetComponent<CharacController> ();
		updateForca (100);
		healthSlider.value = 100;
		ph.currentHealth = ph.startingHealth;
		cc.energia = cc.startingEnergia;

		foreach(SpawPoint s in this.spawPointsScripts){
			s.aumentarDificuldade (1);
			s.reset ();
		}
			
		startGame ();
	}

    public void disableHipolyObjects()
    {
        foreach (Transform t in hiPolyObjects)
        {
            t.gameObject.SetActive(!menuToggle.isOn);
        }
    }


}
