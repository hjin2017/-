using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollCount : MonoBehaviour {
    public int Trollcount;
    public GameObject BoxDoor;
    private Renderer BoxDoorRenderer;
    barchange barchange;
    // Use this for initialization
    void Start () {
        BoxDoorRenderer = BoxDoor.GetComponent<Renderer>();
        barchange = GetComponent<barchange>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DieCount()
    {
        Trollcount--;
        if(Trollcount == 17)
        {
            StartCoroutine("albedo");
        }
    }
    IEnumerator albedo()
    {
        float Ap= BoxDoorRenderer.material.color.a;
        Color boxColor = BoxDoorRenderer.material.color;
        while (true)
        {
            if(Ap<0)
            {
                BoxDoor.SetActive(false);
                barchange.barSetActive();
                StopCoroutine("albedo");
            }
            Ap -= Time.deltaTime;
            BoxDoorRenderer.material.color = new Color(boxColor.r, boxColor.g, boxColor.b, Ap);
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
    }
}
