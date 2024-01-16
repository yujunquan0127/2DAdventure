using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    private PysicsCheck pysicsCheck;
    [Header("Properties")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pysicsCheck = GetComponent<PysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x,0,0);
        
        if(pysicsCheck.touchLeftWall || pysicsCheck.touchRightWall)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDirection.x * Time.deltaTime,rb.velocity.y);
    }
}
