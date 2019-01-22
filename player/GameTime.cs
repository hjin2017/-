using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum winfalse
{
    win,
    winfalse,
    nomal
}
public class GameTime : MonoBehaviour {
    public Text TimeTxt;
    public float StartTime;
    public winfalse Winfalse = winfalse.nomal;
    public Camera main;
    public Camera Sub;

    public GameObject hpbar;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if(Winfalse==winfalse.nomal)
        StartTime -= Time.deltaTime;

        TimeTxt.text = "" + (int)StartTime;
 
        if (StartTime < 0)
        {
            hpbar.SetActive(false);
            main.gameObject.SetActive(false);
            Sub.gameObject.SetActive(true);
            Winfalse = winfalse.winfalse;
            playermove player = transform.GetComponent<playermove>();
            Animator playanim = player.gameObject.GetComponent<Animator>();
            playanim.SetTrigger("winfalse");
            playanim.SetFloat("Speed", 0);
            player.enabled = false;
            StartCoroutine("Endcoroutin");
        }
    }
    IEnumerator Endcoroutin()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("Gmain");
    }
}
