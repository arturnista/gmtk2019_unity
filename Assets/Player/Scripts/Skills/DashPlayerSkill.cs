using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerSkill : MonoBehaviour, IPlayerSkill
{

    public bool Enabled { get; set; }

    [SerializeField]
    private int stage = 2;
    public int Stage { get { return stage; } } 

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float cooldown = 2f;
    private float cooldownPassed = 0f;

    [SerializeField]
    private GameObject ghostPrefab;

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private Vector2 lastPos;
    private bool isDashing;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        cooldownPassed = cooldown;
    }

    void Update()
    {
        if(!Enabled) return;

        cooldownPassed += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && cooldownPassed >= cooldown)  
        {
            playerMovement.ExtraVelocity += playerMovement.MoveDirection * speed;
            isDashing = true;
            playerHealth.IsImmortal = true;
            cooldownPassed = 0f;
        }

        if(isDashing)
        {

            if(playerMovement.ExtraVelocity.sqrMagnitude > 0f)
            {
                if(Vector3.Distance(lastPos, transform.position) >= 1f)
                {
                    lastPos = transform.position;
                    Instantiate(ghostPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                isDashing = false;    
                playerHealth.IsImmortal = false;
            }

        }

    }
}
