using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorManger : MonoBehaviour {

    public GameObject StageObj;
    public GameObject EndObj;
	// Use this for initialization
	void Start () {
        if(StageObj!=null)
        StageObj.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (StageObj != null)
        {
            StageObj.SetActive(true);
            StartCoroutine("istriggercoroutin");
        }
            
        if (EndObj != null)
        EndObj.SetActive(false);
    }
    IEnumerator istriggercoroutin()
    {
        yield return new WaitForSeconds(1.0f);
        transform.GetComponent<Collider>().isTrigger = false;
    }
}
