using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss2d : MonoBehaviour {
    private GAME_MANAGER mANAGER;
    private int Index = 0;
    Image image;
    public Material b0;
    public Material b1;
    public Material b2;
    // Use this for initialization
    void Start () {
        mANAGER = GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
        image = GetComponent<Image>();
       
    }
	
	// Update is called once per frame
	void Update () {
	if(mANAGER.intbar() != Index)
        {
            Index = mANAGER.intbar();
            image.material = swichindex(); 
        }
	}
    Material swichindex()
    {
        switch (Index)
        {
            case 0:
                return b0;
            case 1:
                return b1;
            case 2:
                return b2;
        }
        return null;
    }
}
