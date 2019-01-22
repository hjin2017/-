using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMOvetest : MonoBehaviour {
    private Animator anim;
    private CharacterController CC;
    
    private float MoveSpeed;

    private bool isMove = false;
    private Vector3 MoveDir;
    private float Ang;
    private bool isJump;

    public float MoveSpeedMAx;
    public float JumpSpeed = 8.0f;
    public float Gravity = 9.8f;
    // Use this for initialization
    void Start () {
        MoveSpeed = 0;
        isJump = false;
        anim = GetComponent<Animator>();
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
    }
    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !isJump)
            Ang += h * 3;
        transform.rotation = Quaternion.Euler(0, Ang, 0);

        if (CC.isGrounded)
        {

            isJump = false;

            if (!anim.GetBool("JumpDown"))
                anim.SetBool("JumpDown", true);

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                if (MoveSpeed < MoveSpeedMAx)
                    MoveSpeed += 0.3f;
                anim.SetFloat("animSpeed", MoveSpeed/6);
            }
            else
            {
                if (MoveSpeed > 0)
                    MoveSpeed -= 0.3f;
                if (MoveSpeed < 0)
                    MoveSpeed = 0;
                anim.SetFloat("animSpeed", 1);
            }

            MoveDir = transform.forward * v;
            MoveDir *= MoveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveDir.y = JumpSpeed;
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            if (anim.GetBool("JumpDown"))
                anim.SetBool("JumpDown", false);
            if (Input.GetKeyDown(KeyCode.Space) && !isJump)
            {

                MoveDir = transform.forward * v;
                MoveDir *= MoveSpeed;
                MoveDir.y = JumpSpeed;
                isJump = true;
                anim.SetTrigger("Jump");
            }
        }
        anim.SetFloat("Speed", MoveSpeed + v);

        MoveDir.y -= Gravity * Time.deltaTime;
        CC.Move(MoveDir * Time.deltaTime);
    }
}
