using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barchange : MonoBehaviour {
    public GameObject bar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void barSetActive()
    {
        bar.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag!="Player")return;
        barSetActive();
    }
}
