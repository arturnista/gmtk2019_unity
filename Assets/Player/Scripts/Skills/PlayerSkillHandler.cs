using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHandler : MonoBehaviour
{
    
    private IPlayerSkill[] skills;

    public List<IPlayerSkill> AvailableSkills
    {
        get
        {
            List<IPlayerSkill> ret = new List<IPlayerSkill>();
            foreach (var skill in skills)
            {
                if(skill.Enabled) ret.Add(skill);
            }
            
            return ret;
        }
    }

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
            if(skill.Stage == stage && !skill.Enabled)
            {
                skill.Enabled = true;
                return skill;
            }
        }

        return null;
    }
}