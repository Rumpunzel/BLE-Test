using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActorStateMachine))]
public class ActorVitals : MonoBehaviour
{
    [SerializeField] private float m_MaxHP = 10f;
    [SerializeField] private ActorStateMachine m_StateMachine;

    private float m_HP;


    void Awake()
    {
        m_HP = m_MaxHP;
    }


    public bool Damage(float damage, GameObject sender)
    {
        if (m_HP <= 0f) return false;
        
        m_HP -= m_StateMachine.Damage(damage, m_HP - damage <= 0f);;

        if (m_HP <= 0f) m_StateMachine.Die(sender);
        return true;
    }

    private void ActuallyDie()
    {
        Destroy(gameObject);
    }
}
