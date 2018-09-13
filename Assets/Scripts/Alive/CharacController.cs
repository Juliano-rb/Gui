using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacController : MonoBehaviour
{
    public GameManager gm;
	public QuestManager qm;
    public float speed = 6.0F;
    public float rotateSpeed = 10.0f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
	public float raioGrito = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
	private float rotate = 0f;

	public GameObject projetil;
  	
	public float startingEnergia = 100;
	public float energia = 100;

    public ParticleSystem specialParticle;
    public Transform particleTransform;
	/*
	 * Status do jogo
	 * -Eventos globais
	 */
	public float lancamentocoolDown = 0.2f;
	//public float proxLancamentoCont = 0;
	//Contador regressivo, se 0 então o aluno não esta reclamando
	public float alunoReclamao = 0;
	public float especialCoolDown = 2;

	public float forcaRegen = 1;
	public float energiaReducao = 1;


    void Start()
    {
		energia = startingEnergia;
    }

    void Update()
    {
		if (energia < 100) {
			energia += Time.deltaTime * forcaRegen;
			gm.updateForca (this.energia);
		}

		if (alunoReclamao > 0)
			alunoReclamao -= Time.deltaTime;

		if (especialCoolDown > 0)
			especialCoolDown -= Time.deltaTime;
		
		if (lancamentocoolDown > 0)
			lancamentocoolDown -= Time.deltaTime;
		

		//Se o jogo não está pausado....
		if (Time.deltaTime > 0) {
			if (Input.GetButton ("Fire2")) {
				if (especialCoolDown <= 0) {
					if (energia >= 30) {
						GetComponent<Speech> ().setFalar (true);
						usarEspecial ();
						especialCoolDown = 10;
					}
				}
			} else if (Input.GetButton ("Fire1")) {
				if (lancamentocoolDown <= 0) {
					if (energia >= 5) {
						//GetComponent<Speech> ().setFalar (true);
						atirarObjeto ();
					}
				}
			}
		}
    }
	void usarEspecial(){
		//Passa 10 segundos sem os professores o perseguirem
		this.alunoReclamao += 10;
		this.energia -= 30;

        Destroyer timer = Instantiate(specialParticle, particleTransform.transform).GetComponent<Destroyer>();
        timer.timer = this.alunoReclamao;
        
	}
	void atirarObjeto(){
		this.energia -= 5;
		this.lancamentocoolDown = 0.5f;
		//Posicao inicial do projetil é igual a posicao do personagem porém mais alto
		Vector3 posicao = transform.position;
		posicao.y += 1;

		//posicao = posicao + (transform.forward * -1.2f);

		GameObject proj = Instantiate (projetil, posicao , transform.rotation);

		proj.gameObject.GetComponent<Rigidbody> ().AddForce (transform.forward*-500);
	}
	void FixedUpdate(){
		moveDirection = Vector3.zero;

		moveDirection = new Vector3(0, 0, -Input.GetAxis("Vertical"));

		moveDirection *= speed;

		rotate = 0f;

		rotate = Input.GetAxis ("Horizontal");
		transform.Translate(moveDirection * Time.deltaTime);

		transform.Rotate(0, rotate * rotateSpeed*Time.deltaTime, 0);
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag ("sceneChanger")) {
			int scene = other.gameObject.GetComponent<sceneChanger> ().destinyScene;
			gm.changeScene (scene);
		} else if (other.gameObject.CompareTag ("questEntry")) {
			Quest quest = other.GetComponent<Quest> ();
			//O objeto que colidiu é um objeto de inicio de missão
			if (quest) {
				qm.exibirQuest (quest);
			}

		}
		else if(other.gameObject.CompareTag("questEnd")){
			//O objeto que colidiu é um objeto de fim de missão
			QuestCatch end = other.GetComponent<QuestCatch> ();
			concluirMissao (end);

		}
	}

	void concluirMissao(QuestCatch fimQuest){
		Debug.Log ("Concluiu missao " + fimQuest.questID);
		qm.concluirQuest (fimQuest.questID);
	}

    void OnCollisionEnter(Collision other)
    {
        GameObject gb = other.gameObject;
		//Debug.Log("Colidiu");
		if (gb.CompareTag("Falha"))
        {
            gm.ShowFalha(gb.GetComponent<Falha>());
        }
		if (gb.CompareTag("questEnd"))
		{
			QuestCatch end = other.gameObject.GetComponent<QuestCatch> ();
			concluirMissao (end);
		}

    }
}