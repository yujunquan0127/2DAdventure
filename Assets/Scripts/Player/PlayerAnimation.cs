using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PysicsCheck pysicsCheck;
    private PlayerController playerController;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pysicsCheck = GetComponent<PysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround",pysicsCheck.isGrounded);
        anim.SetBool("isCrouch",playerController.isCrouch);
        anim.SetBool("isDead",playerController.isDead);
        anim.SetBool("isAttack",playerController.isAttack);
    }

    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}
