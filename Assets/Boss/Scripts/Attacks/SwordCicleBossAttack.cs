using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCicleBossAttack : MonoBehaviour, IBossAttack
{

    public bool Finished { get; private set; }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private int amount = 5;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {

        audioSource.PlayOneShot(audioClip, 1f);
        
        Finished = false;

        float angleBase = 360f / amount;
        
        Vector2 playerDirection = (playerHealth.transform.position - transform.position).normalized;
        float initialAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

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

        Invoke("FinishAttack", 1.3f);

    }

    void FinishAttack()
    {
        Finished = true;        
    }

}
