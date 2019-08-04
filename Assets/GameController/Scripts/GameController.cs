using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private GameObject skillIndicatorPrefab;
    [SerializeField]
    private Vector2 skillIndicatorPosition;
    private List<GameObject> indicators;

    private AudioSource audioSource;
    [Header("Audio")]
    [SerializeField]
    private AudioClip gameMusic;
    [SerializeField]
    private AudioClip lobbyMusic;
    [SerializeField]
    private AudioClip startGameClip;
    [SerializeField]
    private AudioClip changeStageClip;

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

        audioSource = GetComponent<AudioSource>();  

        indicators = new List<GameObject>();

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

        audioSource.clip = lobbyMusic;
        audioSource.Play();
    }

    public void OnPlayerDeath()
    {
        gameStarted = false;

        cameraFollower.FinishGame();

        if(bossHealth) Destroy(bossHealth.gameObject);

        mapBlock.SetActive(false);
        
        playerAttack.transform.position = respawnPosition;    

        foreach (var ind in indicators)
        {
            Destroy(ind);
        }    

        Vector3 indicatorPosition = skillIndicatorPosition;
        bool right = true;
        foreach (var skill in playerSkillHandler.AvailableSkills)
        {
            GameObject skillCreated = Instantiate(skillIndicatorPrefab, indicatorPosition, Quaternion.identity);
            skillCreated.GetComponent<SkillIndicator>().Construct(skill);

            if(right) indicatorPosition.x += 4f;
            else 
            {
                indicatorPosition.x -= 4f;
                indicatorPosition.y -= 2f;
            }

            right = !right;
        }

        foreach (var spike in GameObject.FindGameObjectsWithTag("Spike"))
        {
            Destroy(spike);
        }

        audioSource.clip = lobbyMusic;
        audioSource.Play();
    }

    public void OnStartGame()
    {
        if(gameStarted) return;
        gameStarted = true;

        cameraFollower.StartGame();

        playerHealth.TotalHeal();

        GameObject bossCreated = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);

        bossHealth = bossCreated.GetComponent<BossHealth>();
        bossAttackHandler = bossCreated.GetComponent<BossAttackHandler>();

        mapBlock.SetActive(true); 

        CurrentStage = -1;
        NextStage(false);

        audioSource.Stop();
        audioSource.PlayOneShot(startGameClip);
        Invoke("StartGameMusic", 1f);
    }

    void StartGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            OnPlayerDeath();
        }
    }
    
    public void NextStage(bool finalStage)
    {
        if(!finalStage)
        {
            if(CurrentStage >= 0) audioSource.PlayOneShot(changeStageClip, .5f);
            StartCoroutine(NextStageCycle());
        }
        else
        {

            foreach (var spike in GameObject.FindGameObjectsWithTag("Spike"))
            {
                Destroy(spike);
            }

            bossAttackHandler.enabled = false;
            playerSkillHandler.enabled = false;

            bossHealth.Kill();
            Invoke("FadeOut", 3f);
            Invoke("WinScreen", 4f);
        }
    }

    void FadeOut()
    {
        gameHUD.FadeOut();
    }

    void WinScreen()
    {
        SceneManager.LoadScene("WinScreen");
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

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(skillIndicatorPosition, .5f);
        // Gizmos.DrawWireCube(skillIndicatorPosition, Vector3.zero);
    }

}
