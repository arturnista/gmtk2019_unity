using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDamageIndicator : MonoBehaviour
{
    [SerializeField]
    private Color damageColor;

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
            lastHealth = playerHealth.CurrentHealthPoints;

            indicator.color = damageColor;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        Color color = damageColor;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime;
            indicator.color = color;

            yield return null;
        }
    }

}
