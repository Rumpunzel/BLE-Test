using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorState
{
    public string m_AnimatorParameter;

    public ActorStateMachine m_StateMachine
    {
        protected get; set;
    }


    public string Enter()
    {
        return m_AnimatorParameter;
    }

    public string Exit()
    {
        return m_AnimatorParameter;
    }


    virtual public float Move(float direction, bool grounded)
    {
        if (!grounded)
        {
            ChangeToJump();
        }
        else if (Mathf.Abs(direction) > 1f)
        {
            ChangeToWalk();
        }
        else
        {
            ChangeToIdle();
        }
        
        return direction;
    }
    
    virtual public Vector2 Jump(Vector2 jumpForce)
    {
        m_StateMachine.ChangeTo(m_StateMachine.m_JumpState);
        return jumpForce;
    }

    virtual public void Shoot()
    {
        m_StateMachine.ChangeTo(m_StateMachine.m_ShootState);
    }

    virtual protected void ChangeToJump()
    {
        m_StateMachine.ChangeTo(m_StateMachine.m_JumpState);
    }

    virtual protected void ChangeToWalk()
    {
        m_StateMachine.ChangeTo(m_StateMachine.m_WalkState);
    }

    virtual protected void ChangeToIdle()
    {
        m_StateMachine.ChangeTo(m_StateMachine.m_IdleState);
    }
}
