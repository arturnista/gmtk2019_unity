﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    
    private Transform spellContainer;
    private Button continueSpell;

    private Transform dashSpellContainer;
    private Button continueDash;
    private Image fadeoutImage;

    public delegate void Callback();
    Callback showCallback;

    private float delay = 1f;
    private float currentDelay;
    private bool isLocked;

    void Awake()
    {
        spellContainer = transform.Find("SpellContainer");
        spellContainer.gameObject.SetActive(false);

        continueSpell = spellContainer.GetComponentInChildren<Button>();
        continueSpell.onClick.AddListener(HandleContinue);

        fadeoutImage = transform.Find("FadeoutImage").GetComponent<Image>();
    }

    void HandleContinue()
    {
        spellContainer.gameObject.SetActive(false);
        showCallback();
        isLocked = false;
    }

    void Update()
    {
        if(isLocked)
        {
            currentDelay += Time.deltaTime;
            if(currentDelay > delay && Input.anyKeyDown)
            {
                HandleContinue();
            }
        }
    }

    public void ShowSpell(IPlayerSkill playerSkill, Callback callback)
    {
        isLocked = true;
        currentDelay = 0f;

        showCallback = callback;

        TextMeshProUGUI title = spellContainer.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI description = spellContainer.Find("Description").GetComponent<TextMeshProUGUI>();
        Image icon = spellContainer.Find("Icon").GetComponent<Image>();

        title.text = playerSkill.Name;
        description.text = playerSkill.Description;
        icon.sprite = playerSkill.Icon;

        spellContainer.gameObject.SetActive(true);

    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCycle());
    }

    IEnumerator FadeOutCycle()
    {
        Color color = new Color(0f, 0f, 0f, 0f);
        while (true)
        {
            color.a += Time.deltaTime;
            fadeoutImage.color = color;

            yield return null;
        }
    }
    
}
