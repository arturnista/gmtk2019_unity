using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private float delay = 0f;

    private new Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        if(delay > 0f) Invoke("Throw", delay);
        else Throw();
    }

    void Throw()
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