using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDamageIndicator : MonoBehaviour
{
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private Color healColor;

    private PlayerHealth playerHealth;

    private Image indicator;

    private int lastHealth;

    void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        indicator = GetComponent<Image>();
        
        lastHealth = playerHealth.CurrentHealthPoints;
    }

    void Update()
    {
        if(playerHealth && lastHealth != playerHealth.CurrentHealthPoints)
        {
            if(lastHealth < playerHealth.CurrentHealthPoints)
            {
                StartCoroutine(FadeOut(healColor));
            }
            else 
            {
                StartCoroutine(FadeOut(damageColor));
            }
            lastHealth = playerHealth.CurrentHealthPoints;
        }
    }

    IEnumerator FadeOut(Color initialColor)
    {
        indicator.color = initialColor;
        Color color = initialColor;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            indicator.color = color;

            yield return null;
        }
    }

}
