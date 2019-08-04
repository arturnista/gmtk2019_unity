using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillIndicator : MonoBehaviour
{
    
    private TextMeshPro title;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        title = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Construct(IPlayerSkill skill)
    {
        title.text = skill.Name;
        spriteRenderer.sprite = skill.Icon;
    }

}
