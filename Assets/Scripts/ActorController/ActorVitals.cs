using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorStateMachine))]
public class ActorVitals : MonoBehaviour
{
    [SerializeField] private float m_MaxHP = 10f;
    [SerializeField] private ActorStateMachine m_StateMachine;
    [SerializeField] private LayerMask m_WhatDamagesMe;
    [SerializeField] private int m_FlickerAmount = 1;
    [SerializeField] private float m_FlickerInterval = .1f;

    private float m_HP;

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
        Destroy(gameObject);
    }
}
