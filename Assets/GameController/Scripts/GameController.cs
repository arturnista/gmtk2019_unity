using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController main;

    public int CurrentStage { get; private set; }

    private BossHealth bossHealth;
    private BossAttackHandler bossAttackHandler;

    private PlayerSkillHandler playerSkillHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;

    private GameHUD gameHUD;

    void Awake()
    {
        if(main == null) main = this;        

        gameHUD = GameObject.FindObjectOfType<GameHUD>();

        bossHealth = GameObject.FindObjectOfType<BossHealth>();
        bossAttackHandler = GameObject.FindObjectOfType<BossAttackHandler>();

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerSkillHandler = GameObject.FindObjectOfType<PlayerSkillHandler>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

    }

    void Start()
    {
        CurrentStage = -1;
        NextStage();
    }
    
    public void NextStage()
    {
        StartCoroutine(NextStageCycle());
    }

    IEnumerator NextStageCycle()
    {
        CurrentStage += 1;        

        bossAttackHandler.enabled = false;
        playerSkillHandler.enabled = false;
        
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        playerSkillHandler.enabled = false;

        playerHealth.IsImmortal = true;

        IPlayerSkill newSkill = playerSkillHandler.ChangeStage(CurrentStage);
        if(newSkill != null)
        {
            bool playerContinued = false;
            gameHUD.ShowSpell(newSkill, () => playerContinued = true);

            yield return new WaitUntil(() => playerContinued);
        }

        playerHealth.IsImmortal = false;

        bossAttackHandler.enabled = true;
        playerSkillHandler.enabled = true;
        
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        playerSkillHandler.enabled = true;

        bossHealth.ChangeStage(CurrentStage);
        bossAttackHandler.ChangeStage(CurrentStage);
    }

}
