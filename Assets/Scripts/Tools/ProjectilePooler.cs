using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public static ProjectilePooler k_SharedInstance;

    [SerializeField] private GameObject m_ObjectToPool;
    [SerializeField] private int m_AmountToPool;

    private GameObject[] m_PooledObjects;
    private int m_PoolIndex = 0;


    void Awake()
    {
        k_SharedInstance = this;
    }

    void Start()
    {
        m_PooledObjects = new GameObject[m_AmountToPool];

        for (int i = 0; i < m_AmountToPool; i++)
        {
            GameObject newObject = (GameObject)Instantiate(m_ObjectToPool);
            newObject.SetActive(false); 
            newObject.GetComponent<Projectile>().m_PoolIndex = i;
            m_PooledObjects[i] = newObject;
        }
    }


    public GameObject GetPooledProjectile()
    {
        GameObject returnObject = null;

        for (int i = 0; i < m_AmountToPool; i++)
        {
            int index = (i + m_PoolIndex) % m_AmountToPool;
            GameObject nextObject = m_PooledObjects[index];

            if (!nextObject.activeInHierarchy)
            {
                returnObject = nextObject;
                m_PoolIndex = index;
                break;
            }

        }

        if (returnObject == null)
        {
            returnObject = m_PooledObjects[m_PoolIndex];
            returnObject.SetActive(false);
            m_PoolIndex++;
            m_PoolIndex %= m_AmountToPool;
        }

        return returnObject;
    }

    public void DeactivateObject(int index)
    {
        m_PooledObjects[index].SetActive(false);
        m_PoolIndex = index;
    }
}
