﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollAir : MonoBehaviour {

    Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("defence",true);
    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("defence", false);
    }
}
