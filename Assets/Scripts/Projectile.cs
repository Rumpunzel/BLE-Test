using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhoToHit;
    [SerializeField] private float m_Speed = 10f;

    private Rigidbody2D m_Rigidbody2D;


    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Speed, 0f);
    }
}
