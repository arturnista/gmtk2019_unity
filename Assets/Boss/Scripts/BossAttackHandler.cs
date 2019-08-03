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
    private GameObject meleeAttackPrefab;

    private IBossAttack[] attacks;
    private PlayerHealth playerHealth;

    void Awake()
    {
        attacks = GetComponents<IBossAttack>();

        StartCoroutine(AttackCycle());
        StartCoroutine(MeleeAttackCycle());
    }

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        
    }

    IEnumerator MeleeAttackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            float distance = Vector3.Distance(transform.position, playerHealth.transform.position);

            if(distance < 3f) 
            {
                Vector3 direction = Vector3.Normalize(playerHealth.transform.position - transform.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90f);

                Vector3 attackPosition = transform.position + (direction * 2.3f);

                GameObject attackObject = Instantiate(meleeAttackPrefab, attackPosition, rotation);
                attackObject.transform.SetParent(transform);
            }
        }
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            float delay = Random.Range(minAttackDelay, maxAttackDelay);
            yield return new WaitForSeconds(delay);

            IBossAttack attack = attacks[Random.Range(0, attacks.Length)];
            attack.Attack();
        }
    }

}
