using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Enumparticle
{
    Hit,
    Jump,
    End
};
public class playermove : MonoBehaviour {

    private float Speed,Ang;
    private bool Jump,Jump2, dang;
    public float maxSpeed=6;
    public bool isAttck = false;
    public float time = 0;
    ParticleSystem[] particle;
    private bool jumpstat;
    Animator Anim;
    Vector3 lookDir;
    Rigidbody playerrig;

    public GameObject HitParticle;
    public GameObject JumpParticle;
    // Use this for initialization
    void Start () {
        dang = false;
        jumpstat = false;
         Speed = 0;// Jump = true;
         Anim = GetComponent<Animator>();
        playerrig = GetComponent<Rigidbody>();

        particle = new ParticleSystem[(int)Enumparticle.End];

        HitParticle = Instantiate(HitParticle, transform.position,new Quaternion(90,0,0,0));
        HitParticle.transform.parent = transform;
        HitParticle.transform.localScale = new Vector3(40, 40, 40);
        HitParticle.transform.position = HitParticle.transform.position+ new Vector3(0,0.5f,1);
       particle[(int)Enumparticle.Hit] = HitParticle.GetComponent<ParticleSystem>();
        particle[(int)Enumparticle.Hit].Play();
        HitParticle.SetActive(false);

        JumpParticle = Instantiate(JumpParticle, transform.position, new Quaternion(90, 0, 0, 0));
        JumpParticle.transform.parent = transform;
        JumpParticle.transform.position = JumpParticle.transform.position + new Vector3(0, 0.2f, 0);
        particle[(int)Enumparticle.Jump] = JumpParticle.GetComponent<ParticleSystem>();
        particle[(int)Enumparticle.Jump].Play();
        JumpParticle.SetActive(false);
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        if (Speed < maxSpeed && !dang) 
        Speed += 0.05f;
        time += Time.deltaTime;

        if(!isAttck)
        {
            keyupdown();
            keyang();
            jump();
        }
        else
        {
            if(time>3)
            {
                isAttck = false;
            }
            Speed = 0;
        }
        if(playerrig != null)
        playerrig.WakeUp();
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Anim.GetBool("Jump2"))
        {
            playerrig.Sleep();

            if (jumpstat)
                Anim.SetBool("Jump2", true);
          
            playerrig.AddForce(Vector3.up * 9.5f, ForceMode.VelocityChange);
            time = 0;
        }
    }
    void keyang()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            Ang -= 4;
        else if (Input.GetKey(KeyCode.RightArrow))
            Ang += 4;
        transform.rotation = Quaternion.Euler(0, Ang, 0);
    }
    void keyupdown()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Anim.SetFloat("Speed", v + Speed);

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            lookDir = new Vector3(0, 0, v);
            lookDir = transform.TransformDirection(lookDir);
            transform.localPosition += lookDir * Speed * Time.deltaTime;
        }
         else
        {
            if (Speed > 0)
                Speed -= 0.3f;
            if (Speed<0)
                Speed = 0;
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {   //그라운드만
        if (collision.gameObject.tag == "ground")
            StartCoroutine("Jumpparti");
        //몹만
        if (collision.gameObject.tag=="enemy")
        StartCoroutine("Hitparti");
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag== "ground")
        {
            Anim.SetBool("Jump", false);
            Anim.SetBool("Jump2", false);

            jumpstat = false;
            StopCoroutine("changi");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine("changi");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
            StartCoroutine("Hitparti");
    }
    IEnumerator changi()
    {
        yield return new WaitForSeconds(0.05f);
        Anim.SetBool("Jump", true);
        jumpstat = true;
        yield return new WaitForSeconds(3.0f);
        if (Anim.GetBool("Jump")&& Anim.GetBool("Jump2"))
            Anim.SetBool("Jump2", true);
    }
    IEnumerator Jumpparti()
    {
        JumpParticle.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        JumpParticle.SetActive(false);
    }
    IEnumerator Hitparti()
    {
        HitParticle.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        HitParticle.SetActive(false);
    }
}

