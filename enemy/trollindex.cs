using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollindex : MonoBehaviour {
    public GameObject BoxDoor;
    Renderer BoxDoorRenderer;
	// Use this for initialization
	void Start () {
        transform.parent.GetComponent<trollCount>().Trollcount++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DieCount()
    {
        transform.parent.GetComponent<trollCount>().DieCount();
    }

}
