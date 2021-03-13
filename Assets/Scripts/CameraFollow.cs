using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_FollowTransform;
    [SerializeField] private float m_HSmoothSpeed = .8f;
    [SerializeField] private float m_VSmoothSpeed = .5f;
    [SerializeField] private BoxCollider2D m_LevelBounds;

    private float m_XMin, m_XMax, m_YMin, m_YMax;
    private float m_CamY, m_CamX;
    private float m_XBound, m_YBound;
    private Vector3 m_SmoothPos;
    private Camera m_MainCamera;
    

    private void Awake()
    {
        m_MainCamera = GetComponent<Camera>();
        SetBounds();
    }

    void FixedUpdate()
    {
        m_CamX = Mathf.Clamp(m_FollowTransform.position.x, m_XMin + m_XBound, m_XMax - m_XBound);
        m_CamY = Mathf.Clamp(m_FollowTransform.position.y, m_YMin + m_YBound, m_YMax - m_YBound);
        
        m_SmoothPos = new Vector3(
            Mathf.Lerp(this.transform.position.x, m_CamX, m_HSmoothSpeed),
            Mathf.Lerp(this.transform.position.y, m_CamY, m_VSmoothSpeed),
            this.transform.position.z
        );

        this.transform.position = m_SmoothPos;
    }


    private void SetBounds()
    {
        m_XMin = m_LevelBounds.bounds.min.x;
        m_XMax = m_LevelBounds.bounds.max.x;
        m_YMin = m_LevelBounds.bounds.min.y;
        m_YMax = m_LevelBounds.bounds.max.y;
        
        m_YBound = m_MainCamera.orthographicSize;
        m_XBound = m_MainCamera.orthographicSize * m_MainCamera.aspect;
    }
}
