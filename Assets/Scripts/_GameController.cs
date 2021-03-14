using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GameController : MonoBehaviour
{
    public ActorVitals m_PlayerVitals;
    public Text m_LifeCounter;


    void Update()
    {
        m_LifeCounter.text = "Lives: " + m_PlayerVitals.m_HP;

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
