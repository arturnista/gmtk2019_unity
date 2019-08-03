using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    [SerializeField]
    protected GameObject explosionPrefab;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int attackDamage;

    [SerializeField]
    protected float delay = 0f;

    protected new Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        if(delay > 0f) Invoke("Throw", delay);
        else Throw();
    }

    protected virtual void Throw()
    {
        rigidbody2D.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        IHealth health = collider.GetComponent<IHealth>();

        if (health != null)
        {
            health.DealDamage(attackDamage, transform);
        }

        if(explosionPrefab) Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);

    }

}