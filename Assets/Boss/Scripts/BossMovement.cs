using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private float speed; 
    public float Speed { get { return speed; } }

    [SerializeField]
    private float acceleration; 
    public float Acceleration { get { return acceleration; } }

    private Vector3 targetVelocity;
    private PlayerHealth playerHealth;

    // void Awake()
    // {
    //     rigidbody2d = GetComponent<Rigidbody2D>();
    // }

    // void Start()
    // {
    //     playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    // }

    // void Update() 
    // {
    //     Vector2 direction = Vector3.Normalize(playerHealth.transform.position - transform.position);

    //     targetVelocity = direction * speed;

    //     rigidbody2d.velocity = Vector3.MoveTowards(rigidbody2d.velocity, targetVelocity, acceleration * Time.deltaTime);

    // }
}
