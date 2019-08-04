using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private int amount = 2;

    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            for (int i = 0; i < amount; i++)
            {
                
                Vector2 playerDirection = (playerHealth.transform.position - transform.position).normalized;
                float initialAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

                float angleRad = initialAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

                Vector2 spawnPosition = (Vector2)transform.position + (direction * 1.3f);

                Quaternion rotation = Quaternion.Euler(0f, 0f, initialAngle);

                GameObject swordCreated = Instantiate(projectilePrefab, spawnPosition, rotation);

                yield return new WaitForSeconds(.5f);
            }
        }

    }

}
