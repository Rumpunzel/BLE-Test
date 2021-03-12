using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : ActorState
{
    public JumpState()
    {
        m_AnimatorParameter = "Jumping";
    }

    override public void Shoot()
    {
        return;
    }

    override protected void ChangeToJump()
    {
        return;
    }
}
