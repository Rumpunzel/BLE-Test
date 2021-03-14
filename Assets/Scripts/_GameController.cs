using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _GameController : MonoBehaviour
{
    public ActorVitals m_PlayerVitals;
    public Text m_LifeCounter;


    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        m_LifeCounter.text = "Lives: " + m_PlayerVitals.m_HP;

        if (Input.GetKeyDown("backspace"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
