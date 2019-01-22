using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endwall : MonoBehaviour {

    enemyctrl Player;
	// Use this for initialization
	void Start () {
        Player = transform.parent.Find("devil@attack_1").GetComponent<enemyctrl>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Player.HP == 0)
            GetComponent<Collider>().isTrigger = true;
	}
}
