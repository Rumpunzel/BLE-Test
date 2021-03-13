using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ActorInput
{
    override protected void GetInputs()
    {
        m_HorizontalMovement = Input.GetAxisRaw("Horizontal") * m_MovementSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            m_Jumping = true;
        }

        if (Input.GetButtonDown("Shoot"))
        {
            m_Shooting = true;
        }
    }
}
