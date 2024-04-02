using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 0.1f;

    private CapsuleCollider _capsuleCollider; 
    
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public bool IsGrounded()
    {
        var groundCheckDistance = _capsuleCollider.height / 2 + raycastDistance;

        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, groundLayer);
    }
}