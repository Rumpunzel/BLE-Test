using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    [SerializeField] private GameObject m_Projectile;

    override public void Attack(bool facingRight)
    {
        GameObject newProjectile = Instantiate(m_Projectile, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().Fire(facingRight);
    }
}
