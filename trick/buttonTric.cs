using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTric : MonoBehaviour
{
    Vector3 origPos;
    Vector3 beforpos;
    bool Exitpos;
    float time;
    public float Speed=0;
    public GameObject bar;
    private bool startFir=false;
    // Use this for initialization
    void Start()
    {
        origPos = transform.parent.position;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Exitpos)
        {
            if (time < 1)
            {
                time += Time.deltaTime* Speed;
                transform.parent.position = Vector3.Lerp( beforpos, origPos, time);
            }
            else Exitpos = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if(!startFir)
        {
            transform.parent.position = transform.parent.position + new Vector3(0, -Speed * Time.deltaTime, 0);
           
            Exitpos = false;
        }

        if (Vector3.Distance(origPos, transform.position) > 0.1)
        {
            bar.SetActive(true);
            startFir = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        Exitpos = true;
        beforpos = transform.parent.position;
        time = 0;
    }
    public bool GetAcBool() { return startFir;  }
}

