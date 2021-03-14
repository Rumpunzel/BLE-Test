using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorStateMachine : MonoBehaviour
{
    public enum k_STATES {
        Walking,
        Idle,
        Shooting,
        Shooting_2,
        Shooting_3,
        Falling,
        Jumping,
        DoubleJumping,
        Hurt,
        Dead,
    }

    [SerializeField] protected Animator m_Animator;
    [SerializeField] private bool m_HasIFrames = false;
    [SerializeField] private float m_TripleShotCooldown = .4f;

    protected k_STATES m_CurrentState;

    private float m_WeaponCooldown = 0f;


    void Start()
    {
        EnterState(k_STATES.Idle);
    }

    void Update()
    {
        if (m_WeaponCooldown > 0f)
        {
            m_WeaponCooldown = Mathf.Max(m_WeaponCooldown - Time.deltaTime, 0f);
        }
    }

    
    public void ChangeTo(k_STATES newState)
    {
        if (newState == m_CurrentState || m_CurrentState >= k_STATES.Dead) return;

        UpdateAnimator(m_CurrentState, false);
        EnterState(newState);
    }


    public Vector2 Move(Vector2 directionVector, bool grounded)
    {
        // Cannot Move
        if (m_CurrentState >= k_STATES.Hurt || (m_CurrentState >= k_STATES.Shooting && m_CurrentState <= k_STATES.Shooting_3)) return new Vector2(0f, directionVector.y);
        
        k_STATES newState;

        if (!grounded)
        {
            // Transition to at least the Falling State
            newState = m_CurrentState >= k_STATES.Falling ? m_CurrentState : k_STATES.Falling;
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
        if (m_WeaponCooldown > 0f || m_CurrentState >= k_STATES.Shooting_3) return false;

        if (m_CurrentState < k_STATES.Shooting)
        {
            ChangeTo(k_STATES.Shooting);
        }
        else
        {
            if (m_CurrentState == k_STATES.Shooting_2) m_WeaponCooldown = m_TripleShotCooldown;
            
            ChangeTo(m_CurrentState + 1);
        }

        return true;
    }

    public float Damage(float damage)
    {
        // Already Hurting or Dead
        if (m_CurrentState >= k_STATES.Hurt + (m_HasIFrames ? 0 : 1)) return 0f;
        
        return damage;
    }

    public bool Die(GameObject sender)
    {
        // Not Already Dead
        if (m_CurrentState < k_STATES.Dead)
        {
            Vector3 modifiedScale = transform.localScale;
            modifiedScale.x = ((sender.transform.position.x < transform.position.x) ? -1 : 1) * Mathf.Abs(modifiedScale.x);
            transform.localScale = modifiedScale;
            
            ChangeTo(k_STATES.Dead);
        }

        return true;
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
            case k_STATES.Falling:
            case k_STATES.Jumping:
            case k_STATES.DoubleJumping:
            case k_STATES.Hurt:
                break;
            
            case k_STATES.Shooting:
            case k_STATES.Shooting_2:
            case k_STATES.Shooting_3:
                m_Animator.SetTrigger("Shoot");
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
