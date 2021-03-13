using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [SerializeField] private GameObject m_Projectile;

    public void Loose(bool facingRight)
    {
        GameObject newProjectile = Instantiate(m_Projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().Fire(facingRight);
    }
}
