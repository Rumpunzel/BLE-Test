using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ActorStateMachine))]
public class ActorVitals : MonoBehaviour
{
    [SerializeField] private float m_MaxHP = 10f;
    [SerializeField] private ActorStateMachine m_StateMachine;
    [SerializeField] private LayerMask m_WhatDamagesMe;
    [SerializeField] private int m_FlickerAmount = 1;
    [SerializeField] private float m_FlickerInterval = .1f;
    [SerializeField] private bool m_RestartOnDeath = false;

    private float m_HP;
    private float m_KnockBackDistance = 500f;
    private bool m_Flickering = false;
    private int m_TimesFlickered = 0;
    private float m_TimeToFlicker = 0f;
    private bool m_FlickeringOn = true;


    void Awake()
    {
        m_HP = m_MaxHP;
    }

    void Update()
    {
        if (m_Flickering)
        {
            if (m_TimeToFlicker <= 0f)
            {
                Flash();
            }

            m_TimeToFlicker -= Time.deltaTime;

            if (m_TimesFlickered >= m_FlickerAmount * 2)
            {
                m_Flickering = false;
                m_TimesFlickered = 0;
                m_TimeToFlicker = 0f;
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemies"), false);
            }
        }
    }


    public bool Damage(float damage, GameObject sender)
    {
        if (m_HP <= 0f) return false;
        
        float damageTaken = m_StateMachine.Damage(damage);
        m_HP -= damageTaken;
        if (damageTaken > 0f) m_Flickering = true;

        if (m_HP <= 0f) m_StateMachine.Die(sender);
        return true;
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider != null && ((1 << collider.gameObject.layer) & m_WhatDamagesMe) != 0)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, collider.gameObject.layer, true);
            Damage(1f, collider.gameObject);
            Vector2 knockBackVector = (gameObject.transform.position - collider.gameObject.transform.position).normalized * m_KnockBackDistance;
            GetComponent<Rigidbody2D>().AddForce(knockBackVector + new Vector2(knockBackVector.x, Mathf.Min(knockBackVector.y, 200f)));
        }
    }

    private void Flash()
    {
        if(m_FlickeringOn)
        {
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 1f);
        }
        else
        {
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0f);
        }

        m_FlickeringOn = !m_FlickeringOn;
        m_TimesFlickered++;
        m_TimeToFlicker = m_FlickerInterval;

    }

    private void ActuallyDie()
    {
        if (m_RestartOnDeath)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
