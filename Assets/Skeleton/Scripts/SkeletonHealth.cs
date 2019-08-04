using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour, IHealth
{

    public int TotalHealthPoints { get; private set; }
    private int currentHealth;
    [SerializeField]
    private AudioClip deathClip;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool killed;

    void Awake()
    {
        TotalHealthPoints = 2;
        currentHealth = TotalHealthPoints;

        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void DealDamage(int damage, Transform damager)
    {
        if(killed) return;

        currentHealth -= damage;
        if(currentHealth < 0)
        {
            killed = true;

            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(deathClip, 1f);

            spriteRenderer.sprite = null;

            Destroy(GetComponent<SkeletonAttack>());
            Destroy(GetComponent<SkeletonMovement>());
            Destroy(gameObject, 1f);
        }
        else
        {
            StartCoroutine(DamageDisplayCycle());
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

    }

}
