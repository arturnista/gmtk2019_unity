using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController main;

    public int CurrentStage { get; private set; }

    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private GameObject mapBlock;

    [SerializeField]
    private Vector2 respawnPosition;
    [SerializeField]
    private Vector2 bossSpawnPosition;

    private BossHealth bossHealth;
    private BossAttackHandler bossAttackHandler;

    private PlayerSkillHandler playerSkillHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;

    private CameraFollower cameraFollower;

    private bool gameStarted;

    private GameHUD gameHUD;

    void Awake()
    {
        if(main == null) main = this;        

        gameHUD = GameObject.FindObjectOfType<GameHUD>();
        cameraFollower = GameObject.FindObjectOfType<CameraFollower>();

        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        playerSkillHandler = GameObject.FindObjectOfType<PlayerSkillHandler>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

    }

    void Start()
    {
        playerAttack.transform.position = respawnPosition;
    }

    public void OnPlayerDeath()
    {
        gameStarted = false;

        cameraFollower.enabled = true;

        Destroy(bossHealth.gameObject);

        mapBlock.SetActive(false);
        
        playerAttack.transform.position = respawnPosition;        

        foreach (var spike in GameObject.FindGameObjectsWithTag("Spike"))
        {
            Destroy(spike);
        }
    }

    public void OnStartGame()
    {
        if(gameStarted) return;
        gameStarted = true;

        cameraFollower.enabled = false;

        playerHealth.TotalHeal();

        GameObject bossCreated = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);

        bossHealth = bossCreated.GetComponent<BossHealth>();
        bossAttackHandler = bossCreated.GetComponent<BossAttackHandler>();

        mapBlock.SetActive(true); 

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(respawnPosition, .5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bossSpawnPosition, 1f);
    }

}
