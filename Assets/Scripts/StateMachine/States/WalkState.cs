using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : ActorState
{
    public WalkState()
    {
        m_AnimatorParameter = "Walking";
    }

    override protected void ChangeToWalk()
    {
        return;
    }
}
