using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplestEnemy : MonoBehaviour
{
    private Transform targetTransform;
    private Rigidbody2D rb;
    private EnemySorter sorter;

    public float moveSpeed = 4.0f;
    public float repelRange = 1.0f;
    public float repelForce = 10.0f;
    private PlayerLevelTracker levelTracker;
    public int xpValue = 10; // XP the player gains from defeating this enemy


    private float hp = 100;

    void Start()
    {
        if(Mathf.Round(Random.value) == 0)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();    
        }
        else
        {
            targetTransform = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
        }
        
        sorter = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySorter>();
        levelTracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerLevelTracker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTowardsPlayer();
        RepelFromOtherEnemies();
    }

    void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (targetTransform.position - transform.position);
        moveDirection.Normalize();
        rb.velocity = moveDirection * moveSpeed;
    }

    void RepelFromOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repelRange);
        foreach (Collider2D enemy in nearbyEnemies)
        {
            // Ensure the collided object is an enemy and not itself
            if (enemy.gameObject.CompareTag("Enemy") && enemy.gameObject != this.gameObject)
            {
                Vector2 repelDir = (transform.position - enemy.transform.position).normalized;
                var oldMagnitude = rb.velocity.magnitude;
                var newSpeed = rb.velocity + repelDir * repelForce;
                newSpeed.Normalize();
                newSpeed *= oldMagnitude;
                rb.velocity = newSpeed;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }

        if (other.CompareTag("Tower"))
        {
            var behavior = other.GetComponent<TowerBehavior>();
            behavior.TakeDamage(15);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("ProjectileFriendly"))
        {
            if (other.TryGetComponent<IProjectile>(out IProjectile i))
            {
                hp -= i.damage;
                i.dissipationTimer -= i.hitDissipationValue;
                if (hp <= 0)
                {
                    levelTracker.GainXP(10);
                    sorter.enemies.Remove(transform);
                    Destroy(gameObject);
                }
            }
        }
    }
}

