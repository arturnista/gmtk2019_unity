using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingProjectileCollision : ProjectileCollision
{

    private bool isFollowing;

    private Quaternion targetRotation;
    private PlayerHealth playerHealth;

    protected override void Throw()
    {
        base.Throw();
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        isFollowing = true;
    }

    void Update()
    {
        if(isFollowing)
        {
            Vector2 playerDirection = (playerHealth.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);

            rigidbody2D.velocity = transform.right * speed;
        }
    }

}