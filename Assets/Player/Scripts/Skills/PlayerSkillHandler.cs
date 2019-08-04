using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHandler : MonoBehaviour
{
    
    private IPlayerSkill[] skills;

    void Awake()
    {
        skills = GetComponents<IPlayerSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeStage(int stage)
    {
        foreach (var skill in skills)
        {
            if(skill.Stage <= stage) skill.Enabled = true;
            else skill.Enabled = false;
        }
    }
}
