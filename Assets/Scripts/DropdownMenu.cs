using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class DropdownMenu : MonoBehaviour {
	public RectTransform view;
	// Use this for initialization
	
	public void Switch(){
		view.gameObject.SetActive (!view.gameObject.activeSelf);
	}
}
