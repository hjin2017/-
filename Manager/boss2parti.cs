using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2parti : MonoBehaviour {
    private GameObject Parti;
    private boss2Ctrl boss2;
    private GameObject[] particle;
    private int index = 0;
    // Use this for initialization
    void Start () {
        particle = new GameObject[5];
        particle[0] = transform.Find("defenparti").gameObject;

        for (int i=1;i<5;i++)
        {
            particle[i] = transform.Find("defenparti"+i).gameObject;
        }
        for (int i = 0; i < 5; i++)
        {
            particle[i].SetActive(false);
        }

        Parti = transform.Find("pf_vfx-ult_demo_psys_loop_antimatter2").gameObject;
        Parti.transform.position = transform.position;
        Parti.transform.position = Parti.transform.position + new Vector3(0, 6.61f, 0);
        Parti.SetActive(true);
        Parti.GetComponent<ParticleSystem>().Play();
        boss2 = transform.GetComponent<boss2Ctrl>();


    }

    // Update is called once per frame
    private void Update()
    {
        if (boss2.Stat_current == boss2_stat.Nomal)
        {
            Parti.SetActive(true);
            Parti.GetComponent<ParticleSystem>().Play();
        }
        else
            Parti.SetActive(false);


        if(boss2.Stat_current == boss2_stat.Roar)
        {
            for (int i = 0; i < 5; i++)
            {
                particle[i].SetActive(true);
            }
        }

        if (boss2.HP<=0)
            this.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (boss2.Stat_current == boss2_stat.defense)
        {
            particle[index].SetActive(false);
            index++;
        }

        if(boss2.Stat_current == boss2_stat.Die)
        {
            particle[index].SetActive(false);
            index++;
        }
        if (index >= 5)
            index = 0;
    }
}
