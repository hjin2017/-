using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollEye : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = transform.parent.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        anim.SetTrigger("att1");
        anim.SetTrigger("attwin");
        if (collision.gameObject.tag != "Player") return;
        Vector3 dir, Dir;
        collision.rigidbody.Sleep();
        dir = Vector3.Normalize(collision.transform.forward);
        Dir = Vector3.Normalize(-dir + Vector3.up);

        collision.rigidbody.AddForce(Dir * 300);
        collision.transform.position = new Vector3(collision.transform.position.x, 1, collision.transform.position.z);

        Animator panim = collision.gameObject.GetComponent<Animator>();
        panim.SetTrigger("Dang");
        panim.SetFloat("Speed", 0);

        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;
    }

}
