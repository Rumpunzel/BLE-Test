using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : ActorInput
{
    [SerializeField] private GroundCheck m_WallCheck;
    [SerializeField] private GroundCheck m_DropCheck;

    private float m_CheckInterval = .5f;
    private float m_TimeSinceCheck = 0f;


    void Start()
    {
        m_HorizontalMovement = -m_MovementSpeed;
    }

    override protected void GetInputs()
    {
        if (m_TimeSinceCheck < m_CheckInterval)
        {
            m_TimeSinceCheck += Time.deltaTime;
            return;
        }

        if (m_WallCheck.m_IsGrounded || !m_DropCheck.m_IsGrounded) m_HorizontalMovement *= -1f;

        m_TimeSinceCheck = 0f;
    }
}
