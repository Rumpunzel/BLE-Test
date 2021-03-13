using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : ActorInput
{
    [SerializeField] private GroundCheck m_WallCheck;


    void Start()
    {
        m_HorizontalMovement = -m_MovementSpeed;
    }

    override protected void GetInputs()
    {
        if (m_WallCheck.m_IsGrounded) m_HorizontalMovement *= -1f;
    }
}
