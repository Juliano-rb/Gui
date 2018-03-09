using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioSource fonte ;
	public List<Object> mainMusics;
	// Use this for initialization
	void Start () {
		//main = Resources.Load("Musics/FeelGoodInc") as AudioClip;
		Object[] resources = Resources.LoadAll ("Musics/Main");
		mainMusics = new List<Object> (resources);

		int musicIndex = Random.Range(0,mainMusics.Count);

		fonte.clip = (AudioClip) mainMusics[musicIndex];
		fonte.Play ();

		fonte.loop = true;
	}
	
	public void setVolume(float volume){
		this.fonte.volume = volume;
	}
}
