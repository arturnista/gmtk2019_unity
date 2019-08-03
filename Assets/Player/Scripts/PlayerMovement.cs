using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private float speed; 
    public float Speed { get { return speed; } }

    [SerializeField]
    private float acceleration; 
    public float Acceleration { get { return acceleration; } }

    private Vector3 targetVelocity;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    void Update() 
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2 (horizontal, vertical);
        direction.Normalize();

        targetVelocity = direction * speed;

        rigidbody2d.velocity = Vector3.MoveTowards(rigidbody2d.velocity, targetVelocity, acceleration * Time.deltaTime);

    }

    void FixedUpdate()
    {



    }

}
