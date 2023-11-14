using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class TowerBehavior : MonoBehaviour
{
    private float hp = 200;
    private float maxHP = 200;
    private float resistance = 0.1f;
    private float regeneration = 1f;

    private RectTransform hpBar;
    private float startingWidth;
    private float hpToWidthUnit;
    private void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<RectTransform>();
        startingWidth = 1;
        hpToWidthUnit = startingWidth / maxHP;
    }

    private void Update()
    {
        if (hp < maxHP)
        {
            hp += regeneration * Time.deltaTime;
        }
        //Change the width of the hp bar according to current hp
        hpBar.localScale = new Vector3(hp * hpToWidthUnit, hpBar.localScale.y);
        
    }

    void UpdateRegeneration(float regenerationAddition)
    {
        regeneration += regenerationAddition;
    }
    
    public void TakeDamage(float damage)
    {
        hp -= damage * (1 - resistance);
    }
}
