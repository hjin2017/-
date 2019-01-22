using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handbullet : MonoBehaviour {
    // Use this for initialization
    private GameObject Explosion = null;
    private Vector3 origAng;

    private ParticleSystem[] syse = null;

    void Start () {
        origAng = transform.eulerAngles;
    }
    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(0, -10*Time.deltaTime, 0));
        float ang = transform.eulerAngles.x - 360;
        if (ang < -80)
        {
            transform.gameObject.SetActive(false);
            if(Explosion == null)
            {
                Explosion = GetComponent<handParticle>().Explosion;
                transform.eulerAngles = origAng;

                syse = Explosion.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem child in syse)
                {
                    child.Play(true);
                }


                   Explosion.SetActive(true);
            }
            else
            {
                foreach (ParticleSystem child in syse)
                {
                    child.Play(true);
                }

                transform.eulerAngles = origAng;
                Explosion.SetActive(true);
            }
        }
        else if(ang > -10)
        {

        }
    }
}
