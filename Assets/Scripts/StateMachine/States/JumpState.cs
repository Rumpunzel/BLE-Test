using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : ActorState
{
    public JumpState()
    {
        m_AnimatorParameter = "Jumping";
    }

    override public bool Shoot()
    {
        return false;
    }

    override protected void ChangeToJump()
    {
        return;
    }
}
