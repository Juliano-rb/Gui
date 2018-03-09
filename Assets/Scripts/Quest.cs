using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour
{
	public int ID;
	public string title;
	public string descricao;

	public Transform startPosition;
	public Transform endPosition;

	private bool completed;

	// Use this for initialization
	void Start ()
	{
		completed = false;
	}

	public void setInfo(int id, string t, string descrip){
		this.ID = id;
		this.title = t;
		this.descricao = descrip;
	}
	public void complete(){
		this.completed = true;
		gameObject.SetActive (false);
		//Destroy(this.gameObject);
	}

	public void reset(){
		completed = false;
	}

}

