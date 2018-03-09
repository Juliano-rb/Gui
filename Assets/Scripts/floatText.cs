using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatText : MonoBehaviour {
    public Transform target;
	// Update is called once per frame
	void Update () {
        //transform.LookAt()
        //Por algum motivo desconhecido, sem o
        
        transform.LookAt(target);
    }
}
