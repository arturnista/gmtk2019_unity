using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBossAttack : MonoBehaviour, IBossAttack
{
    public bool Finished { get { return rigidbody2D.velocity.sqrMagnitude <= 0f; } }

    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private GameObject telegraphPrefab;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;

    private bool isAttacking;

    private PlayerHealth playerHealth;
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = Vector2.MoveTowards(rigidbody2D.velocity, Vector2.zero, 10f * Time.deltaTime);
    }

    public void Attack()
    {

        audioSource.PlayOneShot(audioClip, 1f);
        StartCoroutine(AttackCycle());

    }

    IEnumerator AttackCycle()
    {
        
        GameObject telegraphCreated = Instantiate(telegraphPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        telegraphCreated.transform.SetParent(transform);

        yield return new WaitForSeconds(.5f);

        Vector2 direction = playerHealth.transform.position - transform.position;
        rigidbody2D.velocity = direction * 2f;

        Destroy(telegraphCreated);
    }

}
