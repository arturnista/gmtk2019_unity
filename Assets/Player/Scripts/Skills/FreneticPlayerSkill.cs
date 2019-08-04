using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreneticPlayerSkill : MonoBehaviour, IPlayerSkill
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
    private float multiplier = 2f;
    [SerializeField]
    private float duration = 5f;
    [SerializeField]
    private float cooldown = 15f;
    private float cooldownPassed;

    private float originalAttackSpeed;

    private PlayerAttack playerAttack;

    
    void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        cooldownPassed = cooldown;
    }

    void Update()
    {
        if(!Enabled) return;

        cooldownPassed += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.F) && cooldownPassed >= cooldown)
        {
            cooldownPassed = 0f;
            StartCoroutine(FreneticCycle());
        }
    }

    IEnumerator FreneticCycle()
    {

        originalAttackSpeed = playerAttack.Cooldown;
        playerAttack.Cooldown = playerAttack.Cooldown / multiplier;

        yield return new WaitForSeconds(duration);

        playerAttack.Cooldown = originalAttackSpeed;

    }

}
