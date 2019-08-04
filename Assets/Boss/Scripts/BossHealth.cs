using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStage
{
    public int Health;
    public Sprite Sprite;
    public Color Color;
}

public class BossHealth : MonoBehaviour, IHealth
{
    
    [SerializeField]
    private List<BossStage> stages;
    public int CurrentStage { get; private set; }

    private SpriteRenderer spriteRenderer;

    private GameController gameController;

    private int currentHealthPoints;
    
    public int TotalHealthPoints { get { return 0; } }
    public int CurrentHealthPoints { get { return currentHealthPoints; } }

    private Coroutine displayCycleCoroutine;
    private Color originalColor;

    private bool isImmortal;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        gameController = GameController.main;        
    }
    
    public void DealDamage(int damage, Transform damager)
    {
        if(isImmortal) return;

        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0) 
        {
            currentHealthPoints = 1000;
            gameController.NextStage();
        }
        else
        {
            if(displayCycleCoroutine != null) StopCoroutine(displayCycleCoroutine);
            displayCycleCoroutine = StartCoroutine(DamageDisplayCycle());

        }

    }

    public void ChangeStage(int stage)
    {
        bool changedStage = CurrentStage != stage;
        CurrentStage = stage;

        currentHealthPoints = stages[CurrentStage].Health;
        originalColor = spriteRenderer.color;

        if(changedStage) 
        {
            StartCoroutine(ChangeStageCycle());
        }
        else
        {
            currentHealthPoints = stages[CurrentStage].Health;
            spriteRenderer.color = stages[CurrentStage].Color;
            spriteRenderer.sprite = stages[CurrentStage].Sprite;
        }

    }

    IEnumerator ChangeStageCycle()
    {

        isImmortal = true;

        float changeStageTime = 2f;
        float amount = 10;

        float time = changeStageTime / amount;

        for (int i = 0; i < amount; i++)
        {
            if(i % 2 == 0) 
            {
                spriteRenderer.color = stages[CurrentStage].Color;
                spriteRenderer.sprite = stages[CurrentStage].Sprite;
            }
            else
            {
                spriteRenderer.color = stages[CurrentStage - 1].Color;
                spriteRenderer.sprite = stages[CurrentStage - 1].Sprite;
            }

            yield return new WaitForSeconds(time);
        }

        currentHealthPoints = stages[CurrentStage].Health;
        spriteRenderer.color = stages[CurrentStage].Color;
        spriteRenderer.sprite = stages[CurrentStage].Sprite;
        
        isImmortal = false;

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
