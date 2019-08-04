using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFinisherHealth : MonoBehaviour, IHealth
{
 
    [SerializeField]
    private int damage;
    [SerializeField]
    private int totalHealthPoints;
    public int TotalHealthPoints { get {return totalHealthPoints;} set {totalHealthPoints = value;} }
    private int currentHealth;

    void Awake()
    {
        currentHealth = TotalHealthPoints;
    }

    void Start()
    {

        StartCoroutine(Finish());

    }

    void Update()
    {
        
    }

    public void DealDamage(int damage, Transform damager)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Finish()
    {
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        yield return new WaitForSeconds(5);

        playerHealth.DealDamage(damage, transform, true);

        Destroy(gameObject);

    }

}
