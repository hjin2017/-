using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletrig : MonoBehaviour {

    public string tagName;
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != tagName) return;
        other.gameObject.SetActive(false);
    }
}
