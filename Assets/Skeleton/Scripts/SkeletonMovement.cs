using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 2f;

    private new Rigidbody2D rigidbody2D;

    private Vector3 direction;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionCycle());
    }

    IEnumerator ChangeDirectionCycle()
    {

        while (true)
        {
            direction = Random.insideUnitCircle;
            rigidbody2D.velocity = direction * speed;
            yield return new WaitForSeconds(5f);

        }

    }
}
