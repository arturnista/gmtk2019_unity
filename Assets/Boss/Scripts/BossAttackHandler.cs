﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField]
    private int contactDamage;
    [SerializeField]
    private AudioClip[] movementAudios;
    private AudioSource audioSource;

    private IBossAttack[] allAttacks;
    private List<IBossAttack> currentStageAttacks;

    private List<IBossAttack> attacksToUse;

    private PlayerHealth playerHealth;

    private Vector3 targetPosition;
    private bool isMoving;

    private Coroutine attackCoroutine;

    private int stage;

    void Awake()
    {
        attacksToUse = new List<IBossAttack>();
        allAttacks = GetComponents<IBossAttack>();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        attackCoroutine = StartCoroutine(AttackCycle());
    }

    void OnDisable()
    {
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }
    
    public void ChangeStage(int stage)
    {
        this.stage = stage;

        currentStageAttacks = new List<IBossAttack>();
        foreach (var attack in allAttacks)
        {
            if(attack.Stage <= stage) currentStageAttacks.Add(attack);
        }

        attacksToUse = new List<IBossAttack>(currentStageAttacks);
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
            if(movementAudios.Length > 0)
            {
                audioSource.clip = movementAudios[Random.Range(0, movementAudios.Length - 1)];
                audioSource.Play();
            }
            
            isMoving = true;
            targetPosition = playerHealth.transform.position + (Vector3)(Random.insideUnitCircle * 3f);
            yield return new WaitForSeconds(1f);
            isMoving = false;

            if(stage < 5)
            {
                IBossAttack attack = ExecuteAttack();
                yield return new WaitUntil(() => attack.Finished);
            }
            else
            {
                IBossAttack[] attacks = new IBossAttack[]
                {
                    ExecuteAttack(),
                    ExecuteAttack()
                };
                foreach (var attack in attacks)
                {
                    yield return new WaitUntil(() => attack.Finished);                    
                }
            }

            yield return new WaitForSeconds(.5f);
        }
    }

    IBossAttack ExecuteAttack()
    {
        if(attacksToUse.Count < 2)
        {
            attacksToUse = new List<IBossAttack>(currentStageAttacks);
        }

        IBossAttack attack = attacksToUse[Random.Range(0, attacksToUse.Count)];
        attack.Attack();

        attacksToUse.Remove(attack);
        
        return attack;
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
