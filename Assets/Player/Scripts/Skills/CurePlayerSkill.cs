using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurePlayerSkill : MonoBehaviour, IPlayerSkill
{
    
    public bool Enabled { get; set; }
    
    [SerializeField]
    private int stage = 1;
    public int Stage { get { return stage; } } 

    [SerializeField]
    private float chargeTime;
    private float currentChargeTime;

    private bool isCharging;

    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    
    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(!Enabled) return;

        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCharging();
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            StopCharging();
        }

        if(isCharging)
        {
            currentChargeTime += Time.deltaTime;
            if(currentChargeTime >= chargeTime)
            {
                CurePlayer();
            }
        }
    }

    void StartCharging()
    {
        isCharging = true;
        
        playerMovement.enabled = false;
        currentChargeTime = 0f;
    }

    void StopCharging()
    {
        isCharging = false;
        playerMovement.enabled = true;
    }

    void CurePlayer()
    {
        playerHealth.AddHealth(2);
        StopCharging();
    }

}
