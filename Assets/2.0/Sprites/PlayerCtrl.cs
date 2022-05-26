using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 100;
    public AudioClip[] JumoClips;
    //public AudioSource audioSource;

    private Transform mGroundCheck;
    Animator anim;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed,
                                            HeroBody.velocity.y);
        }

        anim.SetFloat("speed", Mathf.Abs(h));

        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }
        //���߼����ͨ����λ��Ĳ������ж�����ͨ����==�����������ж�
        if (Physics2D.Linecast(transform.position, mGroundCheck.position,
                                1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

        Debug.DrawLine(transform.position, mGroundCheck.position, Color.red);
    }

    private void FixedUpdate()
    {
        if (bJump)
        {
            //int i = Random.range(0, JumpClips.Length);
            //AudioSource.PlayClipAtPoint(JumoClips[i], transform.position);
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
            anim.SetTrigger("jump");
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}