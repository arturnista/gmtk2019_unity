using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealthContainer : MonoBehaviour
{
    
    [SerializeField]
    private GameObject heartPrefab;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite emptyHeart;

    private List<Image> hearts;

    private PlayerHealth playerHealth;

    private int lastHealth;

    void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    void Start()
    {
        hearts = new List<Image>();

        int amount = Mathf.CeilToInt(playerHealth.TotalHealthPoints / 2f);
        for (int i = 0; i < amount; i++)
        {
            GameObject heartCreated = Instantiate(heartPrefab);
            heartCreated.transform.SetParent(transform);

            Image image = heartCreated.GetComponent<Image>();
            hearts.Add(image);
            image.sprite = fullHeart;
        }
    }

    void Update()
    {
        if(playerHealth && lastHealth != playerHealth.CurrentHealthPoints)
        {
            lastHealth = playerHealth.CurrentHealthPoints;

            for (int i = 0; i < hearts.Count; i++)
            {
                int heartResp = (i + 1) * 2;
                
                Debug.Log(playerHealth.CurrentHealthPoints + " :: " + heartResp);

                if(heartResp <= playerHealth.CurrentHealthPoints)
                {
                    hearts[i].sprite = fullHeart;    
                }
                else if(playerHealth.CurrentHealthPoints % 2 != 0 && playerHealth.CurrentHealthPoints + 1 == heartResp)
                {
                    hearts[i].sprite = halfHeart;                        
                }
                else 
                {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }

}
