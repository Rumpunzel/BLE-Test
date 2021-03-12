using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : ActorState
{
    public ShootState()
    {
        m_AnimatorParameter = "Shooting";
    }

    override public float Move(float direction, bool grounded)
    {
        return 0f;
    }
    
    override public Vector2 Jump(Vector2 jumpForce)
    {
        return Vector2.zero;
    }

    override public void Shoot()
    {
        return;
    }

    public void EndShoot()
    {
        ChangeToIdle();
    }
}
