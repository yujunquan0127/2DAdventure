using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;
    [Header("Parameters")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("Status")]
    public bool isGrounded;

    public bool touchLeftWall;
    public bool touchRightWall;
    public void Update()
    {
        Check();
    }

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2(coll.bounds.size.x/2 + coll.offset.x,coll.offset.y);
            leftOffset = new Vector2(-(coll.bounds.size.x/2 - coll.offset.x), rightOffset.y);
        }
    }

    public void Check()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x* transform.localScale.x,bottomOffset.y), checkRadius, groundLayer);
        rightOffset = new Vector2((coll.bounds.size.x/2+coll.offset.x*transform.localScale.x),coll.offset.y);
        leftOffset = new Vector2(-(coll.bounds.size.x/2-coll.offset.x*transform.localScale.x),rightOffset.y);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
