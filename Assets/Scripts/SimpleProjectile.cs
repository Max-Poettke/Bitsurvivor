using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour, IProjectile
{
    public float speed;
    public float damage { get; set; }
    public float dissipationTimer { get; set; }
    public float hitDissipationValue { get; set; }

    public SimpleProjectile(float nSpeed, float nDamage)
    {
        speed = nSpeed;
        damage = nDamage;
    }
    private void Update()
    {
        Move();
        dissipationTimer -= Time.deltaTime;
        if(dissipationTimer < 0) Destroy(gameObject);
    }

    void Move()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

}
