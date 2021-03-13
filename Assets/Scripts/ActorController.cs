using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorController : MonoBehaviour
{
    [SerializeField] private ActorStateMachine m_StateMachine;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_FallMultiplier = 1.2f;
    [SerializeField] private bool m_CanDoubleJump = false;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private GameObject m_Projectile;


    const float k_GroundedRadius = .2f;
	const float k_CeilingRadius = .2f;

	private Rigidbody2D m_Rigidbody2D;

    private float m_BaseGravity;
    private bool m_Grounded;
    private int m_TimesJumped = 0;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;


    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BaseGravity = m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
                m_TimesJumped = 0;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

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
		if (m_Grounded || (m_CanDoubleJump && m_TimesJumped <= 0))
		{
            Vector2 jumpForce = m_StateMachine.Jump(new Vector2(0f, m_JumpForce));

            if (jumpForce == Vector2.zero) return;

            m_Grounded = false;
            m_TimesJumped++;

            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
			m_Rigidbody2D.AddForce(jumpForce);
		}
	}

    public void Shoot()
    {
		m_StateMachine.Shoot();
	}

    private void Loose()
    {
        // TODO
    }

    private void EndShoot()
    {
		m_StateMachine.EndShoot();
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
