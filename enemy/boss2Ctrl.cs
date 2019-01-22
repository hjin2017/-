using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//이넘 통합?

public enum boss2_stat
{
    Nomal,
    defense,
    Roar,
    Die,
    NoEnemy
};

public class boss2Ctrl : MonoBehaviour {
    public boss2_stat Stat_current = boss2_stat.Nomal;
    public boss2_stat Next_stat = boss2_stat.Nomal;
    public GameObject Hpbar;
    public float HP = 1;
    public bool Life = true;

     GAME_MANAGER bossStage;
    private float MaxHP;
    private Animator anim;
    private float TempHP = 0;

    public int mybossindex;
    public int NextIndex;
    public float waitTime = 5.0f;
   
    // Use this for initialization
    void Start () {

        MaxHP = HP;
        anim = GetComponent<Animator>();
        changeCoroutine(Stat_current);

        bossStage = GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
    }
	
	// Update is called once per frame
	void Update () {
        if (bossStage.intbar() == mybossindex)
        {
            hpAtt(HP / MaxHP);
        }
    }
    void changeCoroutine(boss2_stat nexState)
    {
        if (nexState == boss2_stat.NoEnemy) return;
        StopAllCoroutines();
        Stat_current = nexState;
        anim.SetBool("att", false);
        anim.SetBool("Jump", false);
        anim.SetBool("nomal",false);

        switch (Stat_current)
        {
            case boss2_stat.Nomal: StartCoroutine("coroutineNomal");break;
            case boss2_stat.Roar: StartCoroutine("coroutineRoar"); break;
            case boss2_stat.defense: anim.SetBool("att", true); break;
            case boss2_stat.Die: StartCoroutine("coroutinedie"); break;
        }
    }
    IEnumerator coroutineNomal()
    {
        anim.SetBool("nomal", true);
        while (true)
        {
            yield return new WaitForSeconds(20.0f);
            changeCoroutine(boss2_stat.Roar);
        }
    }
    IEnumerator coroutineRoar()
    {
        anim.SetTrigger("roar");
        yield return new WaitForSeconds(1.0f);
        TempHP = HP;
        changeCoroutine(boss2_stat.defense);
        //이 팩트 빵
        Debug.Log("0");
    }
    IEnumerator coroutinedie()
    {
        if(anim!=null)
        anim.SetTrigger("Die");
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            if ( HP > 0) {
                changeCoroutine(boss2_stat.Nomal);
            }
            else {
                Collider[] coll;
                coll = GetComponents<Collider>();

                foreach(Collider p in coll)
                p.isTrigger = true;
                Life = false;
                yield return new WaitForSeconds(waitTime);

                Hpbar.transform.parent.gameObject.SetActive(false);

                bossStage.setBar(NextIndex);

                StopCoroutine("coroutinedie");
            } 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Stat_current == boss2_stat.Roar) return;//튕기는 이팩
        if (Stat_current == boss2_stat.Die) return;
        switch (Stat_current)
        {
            case boss2_stat.Nomal: {
                    HP -= 5;
                    if (HP <= 0) {
                        nomal_collistion(collision);
                        changeCoroutine(boss2_stat.Die);
                        collision.transform.GetComponent<GameTime>().StartTime += 100;
                        StartCoroutine("AddTime");
                    }
                    else{
                        anim.SetTrigger("hit");
                        nomal_collistion(collision);
                    }

                } break;
            case boss2_stat.defense: {
                    HP--;
                    if (TempHP - HP >= 5){
                        nomal_collistion(collision);

                        changeCoroutine(boss2_stat.Die);
                        if (HP <= 0)
                            StartCoroutine("AddTime");
                        return;
                    }

                    if (Random.Range(0, 4) == 0){
                        anim.SetTrigger("att2");
                        Att2_collistion(collision);
                        Invoke("tauncoll", 1.2f);
                    }
                    else{
                        anim.SetTrigger("att1");
                        Att1_collistion(collision);
                        Invoke("tauncoll", 1.2f);
                    }
                }break;
            case boss2_stat.NoEnemy:
                HP -= 10;
                if(HP<=0)
                {
                    collision.transform.GetComponent<GameTime>().StartTime += 100;
                    StartCoroutine("AddTime");
                    StartCoroutine("coroutinedie");
                }
                break;
        }
    }
    void tauncoll()
    {
        anim.SetTrigger("taunt");
    }
    void Att1_collistion(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        collision.rigidbody.Sleep();

        collision.rigidbody.AddForce(Vector3.Normalize(Vector3.down) * 400);
        collision.rigidbody.AddForce(transform.forward * 400);
        Animator playanim = collision.gameObject.GetComponent<Animator>();
        playanim.SetTrigger("Dang");
        playanim.SetFloat("Speed", 0);

        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;

    }
    void Att2_collistion(Collision collision)
    {

        collision.rigidbody.AddForce(transform.forward * 1000);

        Animator playanim = collision.gameObject.GetComponent<Animator>();
        playanim.SetTrigger("Dang");
        playanim.SetFloat("Speed", 0);
        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;

    }
    void nomal_collistion(Collision collision)
    {
        collision.rigidbody.Sleep();
        collision.rigidbody.AddForce((transform.forward +Vector3.up)* 200);

        Animator playanim = collision.gameObject.GetComponent<Animator>();
        playanim.SetTrigger("Dang");
        playanim.SetFloat("Speed", 0);
        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;

    }

    //매니져에서 보스 위치 체크해죠요 메니져로 함수이동 /

    public void hpAtt(float hp)
    {
        //if(메니져 체크)return;
        if (Hpbar == null) return;
        if (hp < 0) hp = 0;

        Hpbar.transform.localScale = new Vector3(hp,
            Hpbar.transform.localScale.y, 
            Hpbar.transform.localScale.z);
    }
    IEnumerator AddTime()
    {
        Text Time = Hpbar.transform.parent.parent.Find("addtime").GetComponent<Text>();
        Time.gameObject.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        Time.gameObject.SetActive(false);
    }

}
