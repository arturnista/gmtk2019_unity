using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFinisher : MonoBehaviour, IBossAttack
{

    public bool Finished { get; private set; }
    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject swordPrefab;
    private PlayerHealth playerHealth;
    private Vector2 playerPosition;
    private Vector2 direction;
    private Vector2[] spawnPositions;


    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

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

    Finished = false;

        foreach (var spawnPosition in spawnPositions)
        {
            playerPosition = playerHealth.transform.position;

            direction = playerPosition - spawnPosition;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject swordCreated = Instantiate(swordPrefab, spawnPosition, rotation);

        }

    Invoke("FinishAttack", 1.3f);

    }

    void FinishAttack()
    {
        Finished = true;        
    }

}
