using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurePlayerSkill : MonoBehaviour, IPlayerSkill
{
    
    [SerializeField]
    private new string name;
    public string Name { get { return name; } }

    [SerializeField]
    [Multiline]
    private string description;
    public string Description { get { return description; } }

    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }
    
    public bool Enabled { get; set; }
    
    [SerializeField]
    private int stage = 1;
    public int Stage { get { return stage; } } 

    [SerializeField]
    private float chargeTime;
    private float currentChargeTime;
    [SerializeField]
    private AudioClip audioClip;
    private AudioSource audioSource;

    private bool isCharging;

    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    private ParticleSystem system;

    
    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        system = transform.Find("HealParticles").GetComponent<ParticleSystem>();
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
        
        system.Play();
    }

    void StopCharging()
    {
        isCharging = false;
        playerMovement.enabled = true;
        
        system.Stop();
    }

    void CurePlayer()
    {
        audioSource.PlayOneShot(audioClip, 1f);
        
        playerHealth.AddHealth(2);
        StopCharging();
    }

}
