using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSwordCircleBossAttack : MonoBehaviour, IBossAttack
{

    public bool Finished { get; private set; }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject swordPrefab;
    [SerializeField]
    private int amount = 10;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;

    private float initialAngle;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {

        Finished = false;
        
        Vector2 playerDirection = (playerHealth.transform.position - transform.position).normalized;
        initialAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        Wave(initialAngle);

        Invoke("SecondWave", .5f);
        Invoke("FinishAttack", 2.3f);
    }

    void SecondWave()
    {
        int waveAmount = amount / 2;
        float angleBase = 360f / waveAmount;

        Wave(initialAngle + angleBase / 2);
    }

    void Wave(float initialAngle)
    {

        audioSource.PlayOneShot(audioClip, 1f);

        int waveAmount = amount / 2;

        float angleBase = 360f / waveAmount;
        
        for (int i = 0; i < waveAmount; i++)
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

    void FinishAttack()
    {
        Finished = true;        
    }

}
