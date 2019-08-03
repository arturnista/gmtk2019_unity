using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCicleBossAttack : MonoBehaviour, IBossAttack
{
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private int amount = 5;

    public void Attack()
    {


        float angleBase = 360f / amount;
        float initialAngle = Random.Range(0f, 360f);

        for (int i = 0; i < amount; i++)
        {
            
            float angle = initialAngle + angleBase * i;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            Vector2 spawnPosition = (Vector2)transform.position + (direction * 2.3f);

            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject swordCreated = Instantiate(swordPrefab, spawnPosition, rotation);
            swordCreated.transform.SetParent(transform);

        }

    }

}
