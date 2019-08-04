using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePlayerSkill : MonoBehaviour, IPlayerSkill
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
    
    private bool skillEnabled;
    public bool Enabled {
        get {
            return skillEnabled;
        }
        set {
            if(value) playerAttack.ProjectilePrefab = newProjectilePrefab;  
            skillEnabled = value;
        }
    }
    
    [SerializeField]
    private int stage = 1;
    public int Stage { get { return stage; } } 

    [SerializeField]
    private GameObject newProjectilePrefab;
    private PlayerAttack playerAttack;

    
    void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

}
