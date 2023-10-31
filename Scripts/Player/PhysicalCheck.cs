using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalCheck : MonoBehaviour
{
    [Header("Check Parameters")]
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("Status")]
    public bool isGround;

    // Update is called once per frame
    private void Update()
    {
        Check();
    }

    private void Check()
    {
        // ºÏ≤‚µÿ√Ê
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
