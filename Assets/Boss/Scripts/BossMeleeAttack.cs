using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private bool isAttacking;

    private new CircleCollider2D collider2D;

    void Start()
    {
        collider2D = GetComponent<CircleCollider2D>();
        
        isAttacking = false;
        collider2D.enabled = false;
    }

    public void StartAttack()
    {
        isAttacking = true;
        collider2D.enabled = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
        collider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if(isAttacking)
        {
            IHealth health = collider.GetComponent<IHealth>();
            if(health != null) health.DealDamage(damage, transform);
        }
        
    }

}
