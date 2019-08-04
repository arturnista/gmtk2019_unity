using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectileCollision : ProjectileCollision
{

    private BossAttackHandler attackHandler;

    private bool isGoingBack;
    private float timeFlying = 0f;

    protected override void Throw()
    {
        base.Throw();
        attackHandler = GameObject.FindObjectOfType<BossAttackHandler>();
    }

    void Update()
    {

        if(isGoingBack)
        {
            Vector3 direction = attackHandler.transform.position - transform.position;
            if(direction.sqrMagnitude < 5f)
            {
                Destroy(gameObject);
                return;
            }

            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0f, 0f, angle);
            rigidbody2D.velocity = transform.right * speed;
        }
        else
        {
            timeFlying += Time.deltaTime;
            if(timeFlying >= 1f)
            {
                isGoingBack = true;
            }
        }
    }

}