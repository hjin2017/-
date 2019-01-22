using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_STATE
{
    IDLE,
    ATTACK_IDLE,
    FLY,
    DAMAGED,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    Death
}

public class enemyctrl : MonoBehaviour {
    private Animator anim;
    public GameObject hpbar;
    GAME_MANAGER bullet;

    public ENEMY_STATE currState = ENEMY_STATE.IDLE;
    public ENEMY_STATE NextState = ENEMY_STATE.IDLE;
    public float rotationSpeed = 1.0f;
    public float walkSpeed = 1.0f;
    public Vector3 startPosition;
    public int size=1;
    private float MaxHP = 100;

    public float HP;
    Vector3[] tragetposList;
    public int posSize = 7;
    private Vector3 groundpos;
    private Vector3 deathpos;
    private float DeathTime=0;
    private int mybossindex;
    private int bulletCount;
    // Use this for initialization
    void Start () {
        MaxHP= HP;
        bulletCount = 0;
        bullet = GameObject.Find("OBJmanager").GetComponent<GAME_MANAGER>();
        mybossindex = 0;

        anim = GetComponent<Animator>();
        tragetposList = new Vector3[size];

        for (int i = 0; i < size; i++)
        {
            tragetposList[i] = new Vector3(startPosition.x+i* posSize, startPosition.y, startPosition.z);
            tragetposList[i] += transform.parent.position;
        }
        changeCoroutine(currState);
    }
	
	// Update is called once per frame
	void Update () {
        if( bullet.intbar()==mybossindex)
        hpAtt(HP / MaxHP);

        if(HP < 1 )
        {
            if (currState != ENEMY_STATE.Death)
            {
                StopAllCoroutines();
                currState = ENEMY_STATE.Death;
                anim.SetBool("attack_idle", false);
                anim.SetBool("fly", false);
                anim.SetBool("attack_3", false);
                anim.SetBool("attack_2", false);
                anim.SetBool("attack_1", false);
                anim.SetBool("death", true);
                groundpos = transform.position - new Vector3(0, transform.position.y, 0);
                deathpos = transform.position;
            }
            else
            {
                DeathTime += Time.deltaTime*1.1f;
                transform.position = Vector3.Lerp(deathpos, groundpos, DeathTime);
                if (DeathTime > 11)
                    this.gameObject.SetActive(false);
            }
        }
        
    }
    void changeCoroutine(ENEMY_STATE nexState)
    {
        StopAllCoroutines();
        currState = nexState;
        anim.SetBool("attack_idle", false);
        anim.SetBool("fly", false);
        anim.SetBool("attack_3", false);
        anim.SetBool("attack_2", false);
        anim.SetBool("attack_1", false);
        switch (currState)
        {
            case ENEMY_STATE.IDLE: StartCoroutine("Coroutineidle"); break;
            case ENEMY_STATE.ATTACK_IDLE:StartCoroutine("CoroutineAttckidle");break;
            case ENEMY_STATE.ATTACK1: StartCoroutine("CoroutineATTACK1"); break;
            case ENEMY_STATE.ATTACK2: StartCoroutine("CoroutineATTACK2"); break;
            case ENEMY_STATE.ATTACK3: StartCoroutine("CoroutineATTACK3"); break;
            case ENEMY_STATE.FLY: StartCoroutine("CoroutineFLY"); break;
        }
    }

    IEnumerator CoroutineAttckidle()
    {
        anim.SetBool("attack_idle",true);

        
        int rand = Random.Range(0, 4);

        if (bulletCount<5 && rand==2)
        {
            NextState=ENEMY_STATE.ATTACK_IDLE;
        }
        else
        {
            switch (rand)
            {
                case 0: NextState = ENEMY_STATE.ATTACK1; break;
                case 1: NextState = ENEMY_STATE.ATTACK2; break;
                case 2: NextState = ENEMY_STATE.ATTACK1; break;
                case 3: NextState = ENEMY_STATE.IDLE; break;
            }

            yield return new WaitForSeconds(4.0f);
        }

        
        changeCoroutine(NextState);
    }

    IEnumerator Coroutineidle()
    {
        int rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0: NextState = ENEMY_STATE.IDLE; break;
            case 1: NextState = ENEMY_STATE.FLY; break;
            case 2: NextState = ENEMY_STATE.ATTACK_IDLE; break;
        }

        yield return new WaitForSeconds(3.0f);
        changeCoroutine(NextState);
    }

    IEnumerator CoroutineATTACK1()
    {
        anim.SetBool("attack_1", true);

        yield return new WaitForSeconds(1.0f);
        bullet.bulletFir("A1", new Vector3(-9.58f, 0.75f, 88.0f), new Vector3(0, 0, -1));
        bulletCount++;
        NextState = ENEMY_STATE.ATTACK_IDLE;

        changeCoroutine(NextState);
    }
    IEnumerator CoroutineATTACK2()
    {
        anim.SetBool("attack_2", true);

        NextState = ENEMY_STATE.ATTACK_IDLE;

        yield return new WaitForSeconds(1.5f);
        bullet.bulletFir("A2", new Vector3(-0.5f, 0.75f, 88.0f), new Vector3(0, 0, -1));
        bullet.resettrick();

        bulletCount++;
        changeCoroutine(NextState);
    }
   // IEnumerator CoroutineATTACK3()
   // {
   //     anim.SetBool("attack_3", true);
   //     NextState = ENEMY_STATE.ATTACK_IDLE;
   //     yield return new WaitForSeconds(4.0f);
   //     ;
   //     bullet.bulletFir("A3", new Vector3(-0.5f, 1.11f, 70.0f), new Vector3(0, 0, -1));
   //     yield return new WaitForSeconds(8.0f);
   //     bulletCount = 0;
   //     bullet.reSetOBJ("큐브");
   //     changeCoroutine(NextState);
   // }
    IEnumerator CoroutineFLY()
    {
        anim.SetBool("fly", true);

        int rand = Random.Range(0, 3);

        //카메라 포지션 정의되면 포워드 정의 일단 여기서 다른 거 추가 합니다 2018.03.12일

        Vector3 orig = transform.position;
        Vector3 dir =(tragetposList[rand] )- orig;
        //엥글 건드리는 함수
       // transform.forward = Vector3.Normalize(dir); 
        float anitime = 0;
        while (0.2f < Vector3.Distance(transform.position, tragetposList[rand]))
        {
            anitime += Time.deltaTime * 0.2f;
            yield return transform.position = Vector3.Slerp(orig, tragetposList[rand], anitime);
        }
        transform.position = tragetposList[rand];
        NextState = ENEMY_STATE.ATTACK_IDLE;
        changeCoroutine(NextState);
    }

    public void hpAtt(float hp)
    {
        if (hpbar == null) return;
        if (hp < 0) hp = 0;
        hpbar.transform.localScale = new Vector3(hp, hpbar.transform.localScale.y, hpbar.transform.localScale.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        Vector3 dir;
        other.attachedRigidbody.Sleep();

        Vector3 pos1, pos2;
        pos1 = other.transform.position;
        pos1.y = 0;
        pos2 = transform.position;
        pos2.y = 0;
        dir = pos1 - pos2;
        dir = Vector3.Normalize(dir);
        dir.y = 1;
        other.gameObject.GetComponent<Rigidbody>().Sleep();

        other.attachedRigidbody.AddForce(dir * 200);

        Animator anim = other.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Dang");
        anim.SetFloat("Speed", 0);

        other.gameObject.GetComponent<playermove>().isAttck = true;
        other.gameObject.GetComponent<playermove>().time = 0;

        HP -= 10;
    }
}
