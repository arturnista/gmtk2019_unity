using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{


    [SerializeField]
    public int totalHealthPoints;
    [SerializeField]
    private float stopTime;
    [SerializeField]
    private float impulseForce;
    private int currentHealthPoints;
    Rigidbody2D rigidbody2d;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    Vector3 playerPosition;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public bool IsImmortal { get; set; }
    
    public int TotalHealthPoints { get { return totalHealthPoints; } }
    public int CurrentHealthPoints { get { return currentHealthPoints; } }

    void Awake()
    {

        currentHealthPoints = totalHealthPoints;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        playerMovement = GetComponent<PlayerMovement>();
        
        playerAttack = GetComponent<PlayerAttack>();

        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    public void DealDamage(int damage, Transform damager, bool force)
    {
        if(force)
        {
            bool isImmortalBefore = IsImmortal;       
            IsImmortal = false;
            DealDamage(damage, damager);

            IsImmortal = isImmortalBefore;
        }
    }

    public void DealDamage(int damage, Transform damager)
    {
        if(IsImmortal) return;

        currentHealthPoints -= damage;
        StartCoroutine(FreezeMovementAndAttack(damager.position));

        Debug.Log("VIDA DO PREIER: " + currentHealthPoints);

        if (currentHealthPoints <= 0) 
        {

            Destroy(this.gameObject);

        }

    }

    public void AddHealth(int amount)
    {
        if(currentHealthPoints >= TotalHealthPoints) return;
        
        currentHealthPoints += amount;
    }

    IEnumerator FreezeMovementAndAttack(Vector3 enemyPosition)
    {
        IsImmortal = true;

        playerPosition = transform.position;
        Vector3 impuseDirection = playerPosition - enemyPosition;
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        rigidbody2d.velocity = impuseDirection * impulseForce;

        float time = stopTime / 10f;
        
        for (int i = 0; i < 10; i++)
        {
            if(i % 2 == 0) spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
            else spriteRenderer.color = originalColor;

            if(i == 5)
            {
                playerMovement.enabled = true;
                playerAttack.enabled = true;
            }

            yield return new WaitForSeconds(time);
        }

        spriteRenderer.color = originalColor;

        IsImmortal = false;

    }


}
