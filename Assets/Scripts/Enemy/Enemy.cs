using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PysicsCheck))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector]public PysicsCheck pysicsCheck;
    
    [Header("Properties")]
    public float normalSpeed;
    public float chaseSpeed;
    [HideInInspector]public float currentSpeed;
    [HideInInspector]public Vector3 faceDirection;
    [HideInInspector]public Transform attacker;
    public float hurtForce;

    [Header("Detection")] 
    public Vector2 centerOffset;
    public Vector2 checkBoxSize;
    public float checkDistance;
    public LayerMask attackLayer;
    
    [Header("Counter")] 
    public float waitTime;
    [HideInInspector]public float waitCounter;
    [HideInInspector]public bool wait;
    public float lostTime;
    [HideInInspector]public float lostTimeCounter;
    
    [Header("Status")]
    [HideInInspector]public bool ishurt;
    [HideInInspector]public bool isDead;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pysicsCheck = GetComponent<PysicsCheck>();
        
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
        if (!ishurt && !ishurt && !wait)
            Move();
    }

    private void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x,0,0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    public virtual void Move()
    {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SnailPreMove"))
            rb.velocity = new Vector2(currentSpeed * faceDirection.x * Time.deltaTime,rb.velocity.y);
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    //计时器
    public void TimeCounter()
    {
        if (wait)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0 || ishurt)
            {
                wait = false;
                waitCounter = waitTime;
                transform.localScale = new Vector3(faceDirection.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (!FoundPlayer()&&lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;
        }else if (FoundPlayer())
        {
            lostTimeCounter = lostTime;
        }
    }

    #region Event

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
        rb.velocity = new Vector2(0, rb.velocity.y);
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

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkBoxSize, 0, faceDirection,
            checkDistance, attackLayer);
    }
    
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position+(Vector3)centerOffset+
                              new Vector3(checkDistance*-transform.localScale.x,0,0),0.2f);
    }
}
