using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpikesBossAttack : MonoBehaviour, IBossAttack
{
    public bool Finished { get; private set; }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject spikePrefab;
    [SerializeField]
    private int amount = 3;
    [SerializeField]
    private Vector2 mapSize;


    private bool isAttacking;

    private PlayerHealth playerHealth;
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, Vector2.zero, 10f * Time.deltaTime);
    }

    public void Attack()
    {

        Finished = false;

        Vector2 halfMap = mapSize * .5f;

        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-halfMap.x, halfMap.x),
                Random.Range(-halfMap.y, halfMap.y),
                0f
            );
            Instantiate(spikePrefab, spawnPosition, Quaternion.identity);            
        }

        Finished = true;

    }

}
