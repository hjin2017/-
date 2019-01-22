using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class distStreet : MonoBehaviour {
    private float MaxDist;
    private float dist;
    public GameObject distbar;
    public Camera MainCame;
    public Camera SubCame;

    private GameObject player;
    private Vector3 Sv;
    private bool one=false;
    // Use this for initialization
    void Start () {
        Sv = transform.Find("startPoint").GetComponent<Transform>().position;
        Sv.y = 0;
        Vector3 Ev = transform.Find("endPoint").GetComponent<Transform>().position;
        MaxDist = Vector3.Distance(Sv, Ev);
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
       float pDist= Vector3.Distance(Sv, player.transform.position-new Vector3(0, player.transform.position.y, 0));
        distAtt(pDist / MaxDist);
        if(pDist / MaxDist > 1&& !one)
        {
            MainCame.gameObject.SetActive(false);
            SubCame.gameObject.SetActive(true);

            Animator playanim = player.gameObject.GetComponent<Animator>();
            playanim.SetTrigger("win");
            playanim.SetFloat("Speed", 0);
            player.transform.GetComponent<playermove>().enabled = false;

            StartCoroutine("wincoroutin");
            one = true;
        }
    }
    private void distAtt(float hp)
    {
        //if(메니져 체크)return;
        if (hp < 0) hp = 0;
        distbar.transform.localScale = new Vector3(hp, distbar.transform.localScale.y,
            distbar.transform.localScale.z);
    }
    IEnumerator wincoroutin()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Gmain");
    }
}
