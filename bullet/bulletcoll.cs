using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcoll : MonoBehaviour {
    public float power=300;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        collision.rigidbody.Sleep();
        Vector3 dir, Dir;
        dir = Vector3.Normalize(collision.transform.forward);
        Dir = Vector3.Normalize(-dir + Vector3.up);
        collision.transform.position = new Vector3(collision.transform.position.x, 1, collision.transform.position.z);
        collision.rigidbody.AddForce(Dir * power);

        Animator anim = collision.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Dang");
        anim.SetFloat("Speed", 0);

        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;
    }
}
