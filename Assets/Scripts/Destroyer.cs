using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public float timer;
	// Update is called once per frame
	void Update () {
        this.timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
