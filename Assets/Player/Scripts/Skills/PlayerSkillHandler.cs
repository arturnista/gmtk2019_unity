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
    
    public IPlayerSkill ChangeStage(int stage)
    {
        foreach (var skill in skills)
        {
            if(skill.Stage == stage)
            {
                skill.Enabled = true;
                return skill;
            }
        }

        return null;
    }
}