using UnityEngine;
using System.Collections;

public class triggerStage1 : MonoBehaviour {

    public GameObject repos;

    private Vector3 posto;

	// Use this for initialization
	void Start () {
        posto = repos.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = posto;
    }
}
