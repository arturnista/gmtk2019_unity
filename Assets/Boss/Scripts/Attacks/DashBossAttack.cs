using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBossAttack : MonoBehaviour, IBossAttack
{
    public bool Finished { get { return rigidbody2D.velocity.sqrMagnitude <= 1f; } }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private int damage;

    private bool isAttacking;

    private PlayerHealth playerHealth;
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, Vector2.zero, 10f * Time.deltaTime);
    }

    public void Attack()
    {

        Vector2 direction = playerHealth.transform.position - transform.position;
        rigidbody2D.velocity = direction * 2f;

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
