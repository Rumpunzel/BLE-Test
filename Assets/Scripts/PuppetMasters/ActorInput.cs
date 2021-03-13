using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorController))]
public abstract class ActorInput : MonoBehaviour
{
    [SerializeField] private ActorController m_Controller;
    [SerializeField] protected float m_MovementSpeed = 100f;

    protected float m_HorizontalMovement = 0f;
    protected bool m_Jumping = false;
    protected bool m_Attacking = false;


    protected void Update()
    {
        GetInputs();
    }

    protected void FixedUpdate()
    {
        m_Controller.Move(m_HorizontalMovement * Time.fixedDeltaTime);
        
        if (m_Jumping)
        {
            m_Controller.Jump();
            m_Jumping = false;
        }

        if (m_Attacking)
        {
            m_Controller.Attack();
            m_Attacking = false;
        }
    }


    protected abstract void GetInputs();
}
