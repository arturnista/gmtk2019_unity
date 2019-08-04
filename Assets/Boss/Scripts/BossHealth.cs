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

    private BossAttackHandler attackHandler;
    private PlayerSkillHandler playerSkillHandler;

    private int currentHealthPoints;
    
    public int TotalHealthPoints { get { return 0; } }
    public int CurrentHealthPoints { get { return currentHealthPoints; } }

    private Coroutine displayCycleCoroutine;
    private Color originalColor;

    private bool isImmortal;

    void Awake()
    {
        CurrentStage = 0;
        currentHealthPoints = stages[CurrentStage].Health;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackHandler = GetComponent<BossAttackHandler>();
    }

    void Start()
    {
        playerSkillHandler = GameObject.FindObjectOfType<PlayerSkillHandler>();

        CurrentStage = -1;
        NextStage();
    }

    public void DealDamage(int damage, Transform damager)
    {
        if(isImmortal) return;

        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0) 
        {
            isImmortal = true;
            StartCoroutine(ChangeStageCycle());
        }
        else
        {
            if(displayCycleCoroutine != null) StopCoroutine(displayCycleCoroutine);
            displayCycleCoroutine = StartCoroutine(DamageDisplayCycle());

        }

    }

    IEnumerator ChangeStageCycle()
    {

        attackHandler.enabled = false;

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
                spriteRenderer.color = stages[CurrentStage + 1].Color;
                spriteRenderer.sprite = stages[CurrentStage + 1].Sprite;
            }

            yield return new WaitForSeconds(time);
        }

        NextStage();

        isImmortal = false;
        attackHandler.enabled = true;

    }

    void NextStage()
    {
        CurrentStage += 1;

        currentHealthPoints = stages[CurrentStage].Health;
        spriteRenderer.color = stages[CurrentStage].Color;
        spriteRenderer.sprite = stages[CurrentStage].Sprite;

        originalColor = spriteRenderer.color;

        attackHandler.ChangeStage(CurrentStage);
        playerSkillHandler.ChangeStage(CurrentStage);

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
