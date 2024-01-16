using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    
    [Header("Properties")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x,0,0);
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDirection.x * Time.deltaTime,rb.velocity.y);
    }
}
