using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    public bool m_IsGrounded = false;
    

    private void OnTriggerStay2D(Collider2D collider)
    {
        m_IsGrounded = collider != null && ((1 << collider.gameObject.layer) & m_WhatIsGround) != 0;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        m_IsGrounded = false;
    }
}
