using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float cooldown = 0.5f;
    
    private Coroutine fireCoroutine;

    void Awake()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) 
        {
            fireCoroutine = StartCoroutine(FireCycle());
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }

    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(cooldown);
        }
    }

    void Fire()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        direction.Normalize();

        Vector2 attackBornLocation = (Vector2)transform.position + (direction * 2f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f,0f, angle);

        Instantiate(projectilePrefab, attackBornLocation, rotation);

    }

}
