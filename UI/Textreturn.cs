using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Textreturn : MonoBehaviour {
    Text ui;
    private float MyTime = 0;
    // Use this for initialization
    void Start () {
        ui = transform.GetComponentInChildren<Text>();//GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        MyTime += Time.deltaTime;
        Debug.Log(MyTime);
        if (MyTime > 0.5f && MyTime<1)
        {
            Debug.Log("1");
            ui.gameObject.SetActive(false);
        }
        else if(MyTime>1)
        {

            Debug.Log("2");
            MyTime = 0;
            ui.gameObject.SetActive(true);
        }

    }
}
