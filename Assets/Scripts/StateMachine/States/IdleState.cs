using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ActorState
{
    public IdleState()
    {
        m_AnimatorParameter = "Idle";
    }

    override protected void ChangeToIdle()
    {
        return;
    }
}
