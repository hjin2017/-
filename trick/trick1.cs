using UnityEngine;
using System.Collections;
using System.Resources;
public class trick1 : MonoBehaviour {
    public float power=300;
    MeshRenderer renderColor;
    float Alp = 1;
    public float speed = 100f;
    bool coll = false;
   
    // Use this for initialization

    void Start () {
        renderColor = GetComponent<MeshRenderer>();
        renderColor.material.color = new Color(1, 1, 1, Alp);
    }

    // Update is called once per frame
    void Update () {

        if (coll)
        {

            Alp -= speed * Time.deltaTime;
            renderColor.material.color = new Color(1, 1, 1, Alp);
            if (Alp < 0)
            {
                renderColor.material.color = new Color(1, 1, 1, 1);
              this. gameObject.SetActive(false);
                Alp = 1;
                coll = false;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (other.gameObject.GetComponent<playermove>().isAttck) return;
        Vector3 dir,Dir;
        other.attachedRigidbody.Sleep();
        dir = Vector3.Normalize(other.transform.forward);
        Dir = Vector3.Normalize(-dir + Vector3.up);

        other.attachedRigidbody.AddForce(Dir * power);
        other.transform.position = new Vector3(other.transform.position.x, 1, other.transform.position.z);

        Animator anim = other.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Dang");
        anim.SetFloat("Speed", 0);

        other.gameObject.GetComponent<playermove>().isAttck = true;
        other.gameObject.GetComponent<playermove>().time = 0;
        coll = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        collision.rigidbody.Sleep();
        Vector3 dir, Dir;
        dir = Vector3.Normalize(collision.transform.forward);
        Dir = Vector3.Normalize(-dir + Vector3.up);

        collision.transform.position = new Vector3(collision.transform.position.x,1, collision.transform.position.z);
        collision.rigidbody.AddForce(Dir * power);

        Animator anim = collision.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Dang");
        anim.SetFloat("Speed", 0);

        collision.gameObject.GetComponent<playermove>().isAttck = true;
        collision.gameObject.GetComponent<playermove>().time = 0;
        coll = true;
    }
}
