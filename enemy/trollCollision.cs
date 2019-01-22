using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trollCollision : MonoBehaviour {

    Animator anim;
    private Transform Hpbar;
    private SkinnedMeshRenderer albedo;
    public float MaxHp=100;
    public float AttDang=30;
    private float HP;
    private float Alp;

    // Use this for initialization
    void Start () {
         HP= MaxHp;
        anim = transform.parent.GetComponent<Animator>();
        Hpbar= transform.parent.parent.Find("GameObject").Find("hpbar");
        albedo = transform.parent.Find("Ttoll").GetComponent<SkinnedMeshRenderer>();
        Alp = 1;
    }

    // Update is called once per frame
    void Update () {
        if(HP<=0)
        {
            Collider coll;
            Collider eye;
            eye = transform.parent.Find("Atteye").GetComponent<Collider>();
            coll = GetComponent<Collider>();
            coll.isTrigger = true;
            eye.isTrigger = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        HP -= AttDang;
        hpAtt(HP / MaxHp);

        if (HP <= 0)
        {
            StartCoroutine("diecoroutin");

            collision.rigidbody.Sleep();
            Vector3 dir;
            dir = collision.transform.position - transform.parent.position;
            collision.rigidbody.AddForce(Vector3.Normalize(Vector3.up) * 400);
            collision.rigidbody.AddForce(Vector3.Normalize(dir) * 250);
            Animator playanim = collision.gameObject.GetComponent<Animator>();
            playanim.SetTrigger("Dang");
            playanim.SetFloat("Speed", 0);

            collision.gameObject.GetComponent<playermove>().isAttck = true;
            collision.gameObject.GetComponent<playermove>().time = 0;
        }
        else
        {
            anim.SetTrigger("Hit");

            if (collision.gameObject.tag != "Player") return;

            collision.rigidbody.Sleep();
            Vector3 dir;
            dir = collision.transform.position - transform.parent.position;
            collision.rigidbody.AddForce(Vector3.Normalize(Vector3.up) * 400);
            collision.rigidbody.AddForce(Vector3.Normalize(dir) * 250);
            Animator playanim = collision.gameObject.GetComponent<Animator>();
            playanim.SetTrigger("Dang");
            playanim.SetFloat("Speed", 0);

            collision.gameObject.GetComponent<playermove>().isAttck = true;
            collision.gameObject.GetComponent<playermove>().time = 0;

            StartCoroutine("collission");
        }
    }

    IEnumerator collission()
    {
        yield return new WaitForSeconds(1.0f);

        transform.parent.Rotate(new Vector3(0, 180, 0));
    }
    IEnumerator diecoroutin()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        while (true)
        {
            Alp -= 0.2f;
            albedo.material.color = new Color(1, 1, 1, Alp);
            if (Alp < 0)
            {
                StopCoroutine("diecoroutin");
                transform.parent.parent.GetComponent<trollindex>().DieCount();
                transform.parent.parent.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void hpAtt(float hp)
    {
        //if(메니져 체크)return;
        if (hp < 0) hp = 0;
        Hpbar.localScale = new Vector3(hp, Hpbar.localScale.y, Hpbar.localScale.z);
    }
}
