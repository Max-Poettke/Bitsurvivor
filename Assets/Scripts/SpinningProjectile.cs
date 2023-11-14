using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinningProjectile : MonoBehaviour, IProjectile
{
    public float speed;
    public float damage { get; set; }
    public float dissipationTimer { get; set; }
    public float hitDissipationValue { get; set; }

    public Transform player; // Reference to the player
    public float speedToReach = 2f; // Speed at which projectile moves to desired distance
    public float desiredDistance { get; set; }
    private bool isAtDesiredDistance = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the direction from the player to the projectile
        Vector3 toProjectile = transform.position - player.position;
        toProjectile.Normalize();
        if (toProjectile == Vector3.zero)
        {
            toProjectile = Vector3.right;
        }
        

        if (!isAtDesiredDistance)
        {
            // Move the projectile outwards
            if (toProjectile.magnitude < desiredDistance)
            {
                // Normalize the direction and multiply by the desired distance, 
                // then lerp the position of the projectile towards that point
                Vector3 desiredPosition = player.position + toProjectile * desiredDistance;
                transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speedToReach * Time.deltaTime);
            }
            else
            {
                isAtDesiredDistance = true;
            }
        }
    }
}
