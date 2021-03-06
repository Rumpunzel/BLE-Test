using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhoToHit;
    [SerializeField] private LayerMask m_WhereToDestroy;
    [SerializeField] private float m_Damage = 1f;
    [SerializeField] private float m_Speed = 8f;

    public int m_PoolIndex;

    private Rigidbody2D m_Rigidbody2D;


    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void Fire(bool facingRight)
    {
        float directionModifier = facingRight ? 1f : -1f;

        Vector3 modifiedScale = transform.localScale;
		modifiedScale.x = Mathf.Abs(modifiedScale.x) * directionModifier;
		transform.localScale = modifiedScale;

        m_Rigidbody2D.velocity = directionModifier * m_Rigidbody2D.transform.right * m_Speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == null) return;

        if (((1 << collider.gameObject.layer) & m_WhereToDestroy) != 0)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
        }
        else if (((1 << collider.gameObject.layer) & m_WhoToHit) != 0)
        {
            if (collider.GetComponent<ActorVitals>().Damage(m_Damage, gameObject))
            {
                ProjectilePooler.k_SharedInstance.DeactivateObject(m_PoolIndex);
            }
        }
    }
}
