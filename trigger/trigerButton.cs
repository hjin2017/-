using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerButton : MonoBehaviour {
    button pbutton;
    // Use this for initialization
    void Start () {
        pbutton = transform.parent.GetComponent<button>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        pbutton.resetcoroutineTric();
    }
}
