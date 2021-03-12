using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActorController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
