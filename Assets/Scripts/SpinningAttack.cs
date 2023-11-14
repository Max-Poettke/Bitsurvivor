using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class SpinningAttack : MonoBehaviour, IPlayerAttack
{
    private float dissipationTimer = 4000;
    private float timePassed = 0;
    private float speed = 0;

    private List<GameObject> projectiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //delay doesnt matter in this case
        delay = 0;
        damage = 100;
        speed = 100;
        level = 1;
        fireRoutine = StartCoroutine(Fire());
    }

    public void LevelUp()
    {
        level++;
        if (level == 3)
        {
            IncreaseSpeed();
        }
        StopAllCoroutines();
        fireRoutine = StartCoroutine(Fire());
    }
    //float delay is used for setting the delay between attacks
    public float delay { get; set; }
    public float damage { get; set; }
    public int level { get; set; }
    public Vector3 target { get; set; }
    public Coroutine fireRoutine { get; set; }
    public GameObject projectile;

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (projectiles.Count == 0) return;
        foreach (var p in projectiles)
        {
            p.transform.RotateAround(transform.position, p.transform.forward, speed * Time.deltaTime);
        }
    }

    public IEnumerator Fire()
    {
        switch (level)
        {
            case 1:
                level1();
                break;
            case 2:
                level2();
                break;
            case 3:
                level3();
                break;
            case 4:
                level4();
                break;
            default:
                IncreaseSpeed();
                break;
        }
        yield break;
    }

    void level1()
    {
        InstantiateNewProjectile(2f);
    }

    void level2()
    {
        StartCoroutine(WaitForProjectileAlignment(3f, false));
    }
    
    void level3()
    {
        StartCoroutine(WaitForProjectileAlignment(4f, true));
    }

    void level4()
    {
        StartCoroutine(WaitForProjectileAlignment(5f, false));
    }

    IEnumerator WaitForProjectileAlignment(float desiredDistance, bool right)
    {
        if (right)
        {
            while (Vector3.Angle(projectiles.First().transform.position - transform.position, Vector3.right) > 1f)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (Vector3.Angle(projectiles.First().transform.position - transform.position, Vector3.left) > 1f)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        
        
        InstantiateNewProjectile(desiredDistance);
    }

    void InstantiateNewProjectile(float desiredDistance)
    {
        var newProjectile = Instantiate(projectile, transform);
        var sProj = newProjectile.AddComponent<SpinningProjectile>();
        var sRB = newProjectile.AddComponent<Rigidbody2D>();
        sRB.bodyType = RigidbodyType2D.Kinematic;
        sRB.useFullKinematicContacts = true;
        sRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        newProjectile.AddComponent<CircleCollider2D>();
        sProj.desiredDistance = desiredDistance;
        sProj.damage = damage;
        sProj.speed = speed;
        sProj.dissipationTimer = dissipationTimer;
        sProj.hitDissipationValue = 0;
        projectiles.Add(newProjectile);
    }

    void IncreaseSpeed()
    {
        speed += 40;
    }
}