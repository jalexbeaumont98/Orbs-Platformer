using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected int hp;
    [SerializeField] protected int maxHP = 10;
    [SerializeField] protected GameObject deathExplosion;

    protected virtual void Start()
    {
        hp = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }
    
    protected virtual void Die()
    {
        if (deathExplosion)
            Instantiate(deathExplosion, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
