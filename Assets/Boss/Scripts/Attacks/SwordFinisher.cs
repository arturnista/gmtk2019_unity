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
    private GameObject swordFinisherPrefab;
    private PlayerHealth playerHealth;
    private Vector2[] spawnPositions;


    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        spawnPositions = new Vector2[] 
        {
            new Vector2(5f, 5f),
            new Vector2(-5f, 5f),
            new Vector2(5f, -5f),
            new Vector2(-5f, -5f)
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

            GameObject swordCreated = Instantiate(swordFinisherPrefab, spawnPosition, Quaternion.identity);

        }

    Invoke("FinishAttack", 3f);

    }

    void FinishAttack()
    {
        Finished = true;        
    }

}
