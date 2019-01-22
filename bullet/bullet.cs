using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float Speed = 1.5f;
    private Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig=GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        rig.MovePosition(transform.position +transform.forward * Time.deltaTime * Speed);//= //transform.position + transform.forward *Time.deltaTime* Speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;
        Animator anim = collision.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Dang");
        anim.SetFloat("Speed", 0);
    }
}
