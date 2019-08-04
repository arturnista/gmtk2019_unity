using System.Collections;
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

    public delegate void Callback();
    Callback showCallback;

    void Awake()
    {
        spellContainer = transform.Find("SpellContainer");
        spellContainer.gameObject.SetActive(false);

        continueSpell = spellContainer.GetComponentInChildren<Button>();
        continueSpell.onClick.AddListener(HandleContinue);
    }

    void HandleContinue()
    {
        spellContainer.gameObject.SetActive(false);
        showCallback();
    }

    public void ShowSpell(IPlayerSkill playerSkill, Callback callback)
    {

        showCallback = callback;

        TextMeshProUGUI title = spellContainer.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI description = spellContainer.Find("Description").GetComponent<TextMeshProUGUI>();
        Image icon = spellContainer.Find("Icon").GetComponent<Image>();

        title.text = playerSkill.Name;
        description.text = playerSkill.Description;
        icon.sprite = playerSkill.Icon;

        spellContainer.gameObject.SetActive(true);

    }
    
}
