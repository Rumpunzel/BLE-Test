using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    [SerializeField] private GameObject m_Projectile;

    override public void Attack(bool facingRight)
    {
        GameObject bolt = ProjectilePooler.k_SharedInstance.GetPooledProjectile();

        bolt.transform.position = transform.position;
        bolt.transform.rotation = transform.rotation;
        bolt.SetActive(true);
        bolt.GetComponent<Projectile>().Fire(facingRight);
    }
}
