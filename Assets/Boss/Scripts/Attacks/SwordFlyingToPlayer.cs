using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFlyingToPlayer : MonoBehaviour, IBossAttack
{

    public bool Finished { get; private set; }
    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private GameObject indicatorPrefab;
    [SerializeField]
    private float delay = 1f;
    private PlayerHealth playerHealth;
    private Vector2 playerPosition;
    private Vector2 direction;
    private Vector2[] spawnPositions;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;


    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();

        spawnPositions = new Vector2[] 
        {
            new Vector2(4f, 4f),
            new Vector2(-4f, 4f),
            new Vector2(4f, -4f),
            new Vector2(-4f, -4f)
        };

    }

    void Update()
    {
        
    }


    public void Attack() 
    {
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {

        foreach (var spawnPosition in spawnPositions)
        {
            Instantiate(indicatorPrefab, spawnPosition, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(delay);
        
        Finished = false;

        foreach (var spawnPosition in spawnPositions)
        {
            playerPosition = playerHealth.transform.position;

            direction = playerPosition - spawnPosition;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject swordCreated = Instantiate(swordPrefab, spawnPosition, rotation);

            audioSource.PlayOneShot(audioClip);

            yield return new WaitForSeconds(.4f);

        }

        Invoke("FinishAttack", 1.3f);
    }

    void FinishAttack()
    {
        Finished = true;        
    }

}
