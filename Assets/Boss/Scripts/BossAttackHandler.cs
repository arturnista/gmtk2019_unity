﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField]
    private int contactDamage;

    private IBossAttack[] allAttacks;
    private List<IBossAttack> currentStageAttacks;

    private Vector3 targetPosition;
    private bool isMoving;

    void Awake()
    {
        allAttacks = GetComponents<IBossAttack>();

        StartCoroutine(AttackCycle());
    }
    
    public void ChangeStage(int stage)
    {
        currentStageAttacks = new List<IBossAttack>();
        foreach (var attack in allAttacks)
        {
            if(attack.Stage <= stage) currentStageAttacks.Add(attack);
        }
    }

    void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        }
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            isMoving = true;
            targetPosition = transform.position + (Vector3)(Random.insideUnitCircle * 3f);
            yield return new WaitForSeconds(1f);
            isMoving = false;

            IBossAttack attack = currentStageAttacks[Random.Range(0, currentStageAttacks.Count)];
            attack.Attack();
            yield return new WaitUntil(() => attack.Finished);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if(health)
        {
            health.DealDamage(contactDamage, transform);
        }
    }

}
