using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class FireSimple : MonoBehaviour, IPlayerAttack
{
    private float dissipationTimer = 3;
    // Start is called before the first frame update
    private EnemySorter eS;
    void Start()
    {
        delay = 4000;
        damage = 50;
        level = 1;
        eS = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySorter>();
        fireRoutine = StartCoroutine(Fire());
    }
    
    private void Update()
    {
        target = eS.updatedTarget;
    }

    public void LevelUp()
    {
        if (level == 2)
        {
            delay += 500;
        }

        if (level == 4)
        {
            dissipationTimer = 6;
        }

        level++;
    }

    public float delay { get; set; }
    public float damage { get; set; }
    public int level { get; set; }
    public Vector3 target { get; set; }
    public Coroutine fireRoutine { get; set; }
    public GameObject projectile;

    public IEnumerator Fire()
    {
        while (true)
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
                    level2();
                    break;
                default:
                    level4();
                    break;
            }

            yield return new WaitForSeconds(3000 / delay);
        }
    }

    void level1()
    {
        var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        var sProj = newProjectile.AddComponent<SimpleProjectile>();
        var sRB = newProjectile.AddComponent<Rigidbody2D>();
        sRB.bodyType = RigidbodyType2D.Kinematic;
        sRB.useFullKinematicContacts = true;
        sRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        newProjectile.AddComponent<CircleCollider2D>();
        sProj.damage = damage;
        sProj.speed = 30;
        sProj.dissipationTimer = dissipationTimer;
        sProj.hitDissipationValue = 3;
        newProjectile.transform.LookAt(target);
    }

    void level2()
    {
        List<GameObject> projectiles = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);   
            projectiles.Add(newProjectile);
        }

        for (int i = 0; i < projectiles.Count; i++)
        {
            var sProj = projectiles.ElementAt(i).AddComponent<SimpleProjectile>();
            var sRB = projectiles.ElementAt(i).AddComponent<Rigidbody2D>();
            sRB.bodyType = RigidbodyType2D.Kinematic;
            sRB.useFullKinematicContacts = true;
            sRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            projectiles.ElementAt(i).AddComponent<CircleCollider2D>();
            sProj.damage = damage;
            sProj.speed = 30;
            sProj.dissipationTimer = dissipationTimer;
            sProj.hitDissipationValue = 3;
            projectiles.ElementAt(i).transform.LookAt(target);
            projectiles.ElementAt(i).transform.Rotate(Vector3.right, i * 10 - 5);
        }
    }

    void level4()
    {
        List<GameObject> projectiles = new List<GameObject>();
        int projectileAmount = 4;
        for (int i = 0; i < projectileAmount; i++)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);   
            projectiles.Add(newProjectile);
        }

        for (int i = 0; i < projectiles.Count; i++)
        {
            var sProj = projectiles.ElementAt(i).AddComponent<SimpleProjectile>();
            var sRB = projectiles.ElementAt(i).AddComponent<Rigidbody2D>();
            sRB.bodyType = RigidbodyType2D.Kinematic;
            sRB.useFullKinematicContacts = true;
            sRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            projectiles.ElementAt(i).AddComponent<CircleCollider2D>();
            sProj.damage = damage;
            sProj.speed = 30;
            sProj.dissipationTimer = dissipationTimer;
            sProj.hitDissipationValue = 3;
            projectiles.ElementAt(i).transform.LookAt(target);
            projectiles.ElementAt(i).transform.Rotate(Vector3.right, i * 10 - projectileAmount * 5 + 5);
        }
    }
}
