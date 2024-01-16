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
    public Transform attacker;
    public float hurtForce;
    [Header("Counter")] 
    public float waitTime;
    public float waitCounter;
    public bool wait;
    public bool ishurt;
    public bool isDead;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pysicsCheck = GetComponent<PysicsCheck>();
        currentSpeed = normalSpeed;
        waitCounter = waitTime;
    }

    private void FixedUpdate()
    {
        if (!ishurt)
            Move();
    }

    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x,0,0);
        
        if((pysicsCheck.touchLeftWall && faceDirection.x < 0) || (pysicsCheck.touchRightWall && faceDirection.x > 0))
        {
            wait = true;
            anim.SetBool("walk",false);
        }
        TimeCounter();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDirection.x * Time.deltaTime,rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                wait = false;
                waitCounter = waitTime;
                transform.localScale = new Vector3(faceDirection.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        if(attackTrans.position.x > transform.position.x)
            transform.localScale = new Vector3(-1,1,1);
        if(attackTrans.position.x < transform.position.x)
            transform.localScale = new Vector3(1,1,1);
        ishurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2((transform.position.x - attackTrans.position.x), 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        ishurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead",true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}
