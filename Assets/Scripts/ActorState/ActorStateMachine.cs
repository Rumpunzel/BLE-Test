using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorStateMachine : MonoBehaviour
{
    public enum k_STATES {
        Walking,
        Idle,
        Jumping,
        DoubleJumping,
        Shooting,
        Hurt,
        Dead,
    }

    [SerializeField] protected Animator m_Animator;

    protected k_STATES m_CurrentState;


    void Start()
    {
        EnterState(k_STATES.Idle);
    }

    
    public void ChangeTo(k_STATES newState)
    {
        if (newState == m_CurrentState) return;

        UpdateAnimator(m_CurrentState, false);
        EnterState(newState);
    }


    public Vector2 Move(Vector2 directionVector, bool grounded)
    {
        // Cannot Move
        if (m_CurrentState >= k_STATES.Shooting) return new Vector2(0f, directionVector.y);

        k_STATES newState;

        if (!grounded)
        {
            newState = k_STATES.Jumping;
        }
        else if (Mathf.Abs(directionVector.x) > .01f)
        {
            newState = k_STATES.Walking;
        }
        else
        {
            newState = k_STATES.Idle;
        }
        
        if (newState != m_CurrentState) ChangeTo(newState);
        m_Animator.SetBool("Grounded", grounded);
        m_Animator.SetFloat("VerticalSpeed",  directionVector.y);
        
        return directionVector;
    }

    public Vector2 Jump(Vector2 jumpForce)
    {
        // Cannot Jump
        if (m_CurrentState > k_STATES.Jumping) return Vector2.zero;

        ChangeTo(m_CurrentState == k_STATES.Jumping ? k_STATES.DoubleJumping : k_STATES.Jumping);
        return jumpForce;
    }

    public bool Attack()
    {
        // Cannot Attack
        if (m_CurrentState > k_STATES.Idle) return false;

        ChangeTo(k_STATES.Shooting);
        return true;
    }

    public float Damage(float damage)
    {
        // Already Hurting or Dead
        if (m_CurrentState >= k_STATES.Hurt) return 0f;

        ChangeTo(k_STATES.Hurt);
        return damage;
    }

    public bool Die()
    {
        // Already Dead
        if (m_CurrentState < k_STATES.Dead) ChangeTo(k_STATES.Dead);
        return true;
    }

    public void ReturnToIdle()
    {
        ChangeTo(k_STATES.Idle);
    }


    private void EnterState(k_STATES newState)
    {
        m_CurrentState = newState;
        UpdateAnimator(newState, true);
    }

    private void UpdateAnimator(k_STATES state, bool status)
    {
        switch(state)
        {
            case k_STATES.Jumping:
            case k_STATES.DoubleJumping:
                break;
            case k_STATES.Dead:
                m_Animator.SetTrigger("Die");
                break;
            default:
                m_Animator.SetBool(state.ToString(), status);
                break;
        }
    }
}
