using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField]
    private float minAttackDelay;
    [SerializeField]
    private float maxAttackDelay;
    [SerializeField]
    private int contactDamage;

    private IBossAttack[] allAttacks;
    private List<IBossAttack> currentStageAttacks;

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

    IEnumerator AttackCycle()
    {
        while (true)
        {
            float delay = Random.Range(minAttackDelay, maxAttackDelay);
            yield return new WaitForSeconds(1f);

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
