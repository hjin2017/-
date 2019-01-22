using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {
    public Vector3 position;
    GAME_MANAGER _MANAGER;
    buttonTric TriS;
    bool fir=false;
    // Use this for initialization
    void Start () {
        _MANAGER = GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
        TriS = GetComponent<buttonTric>();
      
    }
	
	// Update is called once per frame
	void Update () {
        if(TriS.GetAcBool()&& !fir)
        {
            fir = true;
            StartCoroutine("CoroutineTric");
        }
    }
    IEnumerator CoroutineTric()
    {
        int y = 0;
        while (true)
        {
            int TricIndex = Random.Range(0, 4);
            for (int i = 0; i< 4; i++)
            {
                if(TricIndex==i)
                _MANAGER.bulletFir("A4", position+ new Vector3( i * 3.5f, 0,y*7.2f), Vector3.up);
            }
           
            yield return new WaitForSeconds(1);
            y++;
            if (y == 37) break;
        }
        yield return new WaitForSeconds(7);
        _MANAGER.bulletrest("A4");
    }
    public void resetcoroutineTric()
    {
        _MANAGER.bulletrest("A4");
        StopCoroutine("CoroutineTric");
        Invoke("startInvork",1.0f);
    }
    void startInvork()
    {
        StartCoroutine("CoroutineTric");
    }
}
