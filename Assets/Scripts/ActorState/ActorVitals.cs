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


    public void Damage(float damage, GameObject sender)
    {
        m_HP -= m_StateMachine.Damage(damage);;

        if (m_HP <= 0f) m_StateMachine.Die(sender);
    }

    private void ActuallyDie()
    {
        Destroy(gameObject);
    }
}
