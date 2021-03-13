using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ActorStateMachine))]
public class ActorController : MonoBehaviour
{
    [SerializeField] private ActorStateMachine m_StateMachine;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_FallMultiplier = 1.2f;
    [SerializeField] private bool m_CanDoubleJump = false;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private GroundCheck m_GroundCheck;
	[SerializeField] private Weapon m_Weapon;

	private Rigidbody2D m_Rigidbody2D;

    private float m_BaseGravity;
    private bool m_Grounded;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;


    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BaseGravity = m_Rigidbody2D.gravityScale;
	}

    private void FixedUpdate()
	{
		m_Grounded = m_GroundCheck.m_IsGrounded;
		
        m_Rigidbody2D.gravityScale = m_BaseGravity * (m_Rigidbody2D.velocity.y < 0 ? m_FallMultiplier: 1f);

        m_StateMachine.Move(m_Rigidbody2D.velocity, m_Grounded);
	}

    public void Move(float direction)
	{
        Vector3 targetVelocity = m_StateMachine.Move(new Vector2(direction, m_Rigidbody2D.velocity.y), m_Grounded);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if ((targetVelocity.x > 0 && !m_FacingRight) || (targetVelocity.x < 0 && m_FacingRight))
        {
            Flip();
        }
    }

    public void Jump()
    {
		if (m_Grounded || m_CanDoubleJump)
		{
            Vector2 jumpForce = m_StateMachine.Jump(new Vector2(0f, m_JumpForce));

            if (jumpForce == Vector2.zero) return;

            m_Grounded = false;

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
			m_Rigidbody2D.AddForce(jumpForce);
		}
	}

    public void Attack()
    {
		m_StateMachine.Attack();
	}

    private void WeaponAttack()
    {
		m_Weapon.Attack(m_FacingRight);
    }

    private void ReturnToIdle()
    {
		m_StateMachine.ReturnToIdle();
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 modifiedScale = transform.localScale;
		modifiedScale.x *= -1;
		transform.localScale = modifiedScale;
	}
}
