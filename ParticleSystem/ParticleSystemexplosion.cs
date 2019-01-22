using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemexplosion : MonoBehaviour {
    public GameObject explosion;
    private ParticleSystem syse=null;
    // Use this for initialization
    void Start()
    {
    }
	// Update is called once per frame
	void Update () {
    }
    private void OnTriggerEnter(Collider other)
    {if (other.gameObject.tag != "Player") return;
        if(syse==null)
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            syse = explosion.GetComponent<ParticleSystem>();
            syse.Play(true);
        }
        else
            syse.Play(true);
    }
}
