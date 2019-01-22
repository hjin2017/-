using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windoor : MonoBehaviour {

   private boss2Ctrl boss;
   private bool door = false;
   private float ang = 0;
    // Use this for initialization
    void Start () {
        boss = transform.parent.GetComponent<boss2Ctrl>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!boss.Life&&!door){
            transform.Rotate(new Vector3(0, 0.5f, 0));
            if (transform.eulerAngles.y >= 90)
                door = true;
        }
	}
}
