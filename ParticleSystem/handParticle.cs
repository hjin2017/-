using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handParticle : MonoBehaviour {

    public GameObject Explosion;
    public GameObject Prefab;

    // Use this for initialization
    void Start () {
        Explosion = GameObject.Instantiate(Prefab) as GameObject;
        Explosion.transform.position = transform.parent.Find("bossatt3pos").position;
        Explosion.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
