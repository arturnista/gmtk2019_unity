using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSwordsBossAttack : MonoBehaviour, IBossAttack
{

    public bool Finished { get; private set; }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private int amount = 3;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    public void Attack()
    {

        Finished = false;
        
        StartCoroutine(AttackCycle());

    }

    IEnumerator AttackCycle()
    {

        for (int i = 0; i < amount; i++)
        {
            
            Vector2 playerDirection = (playerHealth.transform.position - transform.position).normalized;
            float initialAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            float angleRad = initialAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            Vector2 spawnPosition = (Vector2)transform.position + (direction * 2.3f);

            Quaternion rotation = Quaternion.Euler(0f, 0f, initialAngle);

            GameObject swordCreated = Instantiate(swordPrefab, spawnPosition, rotation);
            swordCreated.transform.SetParent(transform);

            yield return new WaitForSeconds(1f);

        }

        Finished = true;
    }

}
