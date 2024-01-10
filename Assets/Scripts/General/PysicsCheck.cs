using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicsCheck : MonoBehaviour
{
    [Header("Parameters")]
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("Status")]
    public bool isGrounded;
    public void Update()
    {
        Check();
    }

    public void Check()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
