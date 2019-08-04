using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFinisherHealth : MonoBehaviour, IHealth
{
 
    [SerializeField]
    private int damage;
    [SerializeField]
    private Sprite spriteFive;
    [SerializeField]
    private Sprite spriteFour;
    [SerializeField]
    private Sprite spriteThree;
    [SerializeField]
    private Sprite spriteTwo;
    [SerializeField]
    private Sprite spriteOne;
    [SerializeField]
    private Sprite spriteDeath;
    [SerializeField]
    private int totalHealthPoints;
    public int TotalHealthPoints { get {return totalHealthPoints;} set {totalHealthPoints = value;} }
    private int currentHealth;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = transform.Find("TimerSprite").GetComponent<SpriteRenderer>();
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

        for (int i = 0; i<6;i++)
        {
            if(i == 0) 
            {
                spriteRenderer.sprite = spriteFive;
            }
            else if(i == 1) 
            {
                spriteRenderer.sprite = spriteFour; 
            }
            else if(i == 2)
            {
                spriteRenderer.sprite = spriteThree;
            }
            else if(i == 3)
            {
                spriteRenderer.sprite = spriteTwo;
            }
            else if(i == 4)
            {
                spriteRenderer.sprite = spriteOne;
            }
            else if(i == 5)
            {
                spriteRenderer.sprite = spriteDeath;
            }

            yield return new WaitForSeconds(1);

        }



        playerHealth.DealDamage(damage, transform, true);

        Destroy(gameObject);

    }

}
