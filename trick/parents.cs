using UnityEngine;
using System.Collections;

public class parents : MonoBehaviour {
    float MyR;
    float MyG;
    float MyB;

    // Use this for initialization
    void Start () {
        MyR = this.GetComponent<MeshRenderer>().material.color.r;
        MyG = this.GetComponent<MeshRenderer>().material.color.g;
        MyB = this.GetComponent<MeshRenderer>().material.color.b;
        this.GetComponent<MeshRenderer>().material.color = new Color(MyR, MyG, MyB, this.transform.parent.gameObject.GetComponent<MeshRenderer>().material.color.a);
    }
	
	// Update is called once per frame
	void Update () {
      
        this.GetComponent< MeshRenderer >().material.color=  new Color(MyR, MyG, MyB, this.transform.parent.gameObject.GetComponent<MeshRenderer>().material.color.a);

    }
}
