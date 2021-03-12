using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateMachine : MonoBehaviour
{
    [SerializeField] protected Animator m_Animator;

    public IdleState m_IdleState = new IdleState();
    public WalkState m_WalkState = new WalkState();
    public JumpState m_JumpState = new JumpState();
    public ShootState m_ShootState = new ShootState();
    public DeadState m_DeadState = new DeadState();

    protected ActorState m_CurrentState;

    private ActorState[] m_States;


    void Awake()
    {
        m_States = new ActorState[]{
            m_IdleState,
            m_WalkState,
            m_JumpState,
            m_ShootState,
            m_DeadState,
        };
    }

    void Start()
    {
        foreach (ActorState state in m_States)
        {
            state.m_StateMachine = this;
        }

        EnterState(m_IdleState);
    }

    
    public void ChangeTo(ActorState newState)
    {
        if (newState == m_CurrentState) return;
        
        UpdateAnimator(m_CurrentState.Exit(), false);
        EnterState(newState);
    }


    public Vector2 Move(Vector2 directionVector, bool grounded)
    {
        m_Animator.SetFloat("VerticalSpeed",  directionVector.y);

        return new Vector2(m_CurrentState.Move(directionVector.x, grounded), directionVector.y);
    }

    public Vector2 Jump(Vector2 jumpForce)
    {
        return m_CurrentState.Jump(jumpForce);
    }

    public void Shoot()
    {
        m_CurrentState.Shoot();
    }

    public void EndShoot()
    {
        Debug.Assert(m_CurrentState == m_ShootState);
        ((ShootState)m_CurrentState).EndShoot();
    }


    private void EnterState(ActorState newState)
    {
        m_CurrentState = newState;
        UpdateAnimator(m_CurrentState.Enter(), true);
    }

    private void UpdateAnimator(string state, bool status)
    {
        if (!state.Equals("Jumping"))
        {
            m_Animator.SetBool(state, status);
        }
    }
}
