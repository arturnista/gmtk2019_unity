using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour, IHealth
{

    [SerializeField]
    private int damage;

    public int TotalHealthPoints { get; private set; }
    private int currentHealth;

    void Awake()
    {
        TotalHealthPoints = 3;
        currentHealth = TotalHealthPoints;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerHealth health = collider.GetComponent<PlayerHealth>();
        if(health)
        {
            health.DealDamage(damage, transform);
        }
    }

    public void DealDamage(int damage, Transform damager)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

}
