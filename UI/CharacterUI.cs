using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour {

    public GameObject prefab;
    public GameObject Group;
    public Vector3 postion;
    private GameObject mnue;
    private bool select=false;
   private CharacterUI[] arrUI;
    // Use this for initialization
    void Start () {
        mnue= GameObject.Instantiate(prefab) as GameObject;
        mnue.transform.parent = Group.transform;
        mnue.transform.position = postion + Group.transform.position;

        for (int i = 0; i < transform.parent.childCount; i++)
            arrUI = transform.parent.GetComponentsInChildren<CharacterUI>();

        mnue.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
   public void Onclick() {
        if(!select){
            for (int i = 0; i < transform.parent.childCount-1; i++)
                arrUI[i].isFails();

            mnue.SetActive(true);
            select = true;
        }
        else
            isFails();
    }
    public void isFails()
    {
        mnue.SetActive(false);
        select = false;
    }

}
