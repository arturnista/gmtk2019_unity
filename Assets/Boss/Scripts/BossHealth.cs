using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IHealth
{
    
    [SerializeField]
    public int totalHealthPoints;

    private SpriteRenderer spriteRenderer;

    private int currentHealthPoints;
    
    public int TotalHealthPoints { get { return totalHealthPoints; } }
    public int CurrentHealthPoints { get { return currentHealthPoints; } }

    private Coroutine displayCycleCoroutine;
    private Color originalColor;

    void Awake()
    {
        currentHealthPoints = totalHealthPoints;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void DealDamage(int damage, Transform damager)
    {
        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(displayCycleCoroutine != null) StopCoroutine(displayCycleCoroutine);
            displayCycleCoroutine = StartCoroutine(DamageDisplayCycle());

        }

    }

    IEnumerator DamageDisplayCycle()
    {

        for (int i = 0; i < 10; i++)
        {
            if(i % 2 == 0) spriteRenderer.color = Color.white;
            else spriteRenderer.color = originalColor;

            yield return new WaitForSeconds(.07f);
        }

        spriteRenderer.color = originalColor;
        displayCycleCoroutine = null;

    }
}
