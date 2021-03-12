using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : ActorState
{
    public DeadState()
    {
        m_AnimatorParameter = "Die";
    }

    
    override public float Move(float direction, bool grounded)
    {
        return 0f;
    }
    
    override public Vector2 Jump(Vector2 jumpForce)
    {
        return Vector2.zero;
    }

    override public bool Shoot()
    {
        return false;
    }
}
