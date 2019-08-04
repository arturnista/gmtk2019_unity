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
    private Vector3 moveVelocity;
    public Vector3 MoveDirection { get; set; }
    public Vector3 ExtraVelocity { get; set; }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    void OnDisable()
    {
        rigidbody2d.velocity = Vector2.zero;
    }

    void Update() 
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        MoveDirection = (new Vector2 (horizontal, vertical)).normalized;

        targetVelocity = MoveDirection * speed;

        moveVelocity = Vector3.MoveTowards(moveVelocity, targetVelocity, acceleration * Time.deltaTime);
        
        rigidbody2d.velocity = moveVelocity + ExtraVelocity;

        ExtraVelocity = Vector3.MoveTowards(ExtraVelocity, Vector3.zero, 150f * Time.deltaTime);

    }

    void FixedUpdate()
    {



    }

}
