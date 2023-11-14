using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> enemies;
    public GameObject enemy;
    public List<Transform> targetTransforms;
    public Transform targetTransform = null;
    public float spawningDistance;
    private float timePassed;
    private float spawnFactor;
    private EnemySorter eSorter;
    
    private bool waveInProgress = false;
    

    private void Start()
    {
        timePassed = 0;
        spawnFactor = 1000;
        eSorter = GetComponent<EnemySorter>();
        targetTransforms.Add(GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>());
        targetTransforms.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        StartCoroutine(SpawnEnemies());
        
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        spawnFactor = 1000 + timePassed * 10;
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            SpawnEnemy();
            
            yield return new WaitForSeconds(1000 / spawnFactor);
        }
    }

    void SpawnEnemy()
    {
        if (waveInProgress) targetTransform = targetTransforms.ElementAt(0);
        else targetTransform = targetTransforms.ElementAt(1);
    
        Vector3 spawnPosition;
        int attempts = 0;
    
        do {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            Vector2 spawnDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            spawnPosition = targetTransform.position + new Vector3(spawnDirection.x, spawnDirection.y, 0) * spawningDistance;
            attempts++;
        } while ((MeasureDistance(targetTransforms.ElementAt(0).position, spawnPosition) < 5 
                  || MeasureDistance(targetTransforms.ElementAt(1).position, spawnPosition) < 5) && attempts < 100);
    
        if(attempts >= 100)
        {
            Debug.LogError("Failed to find a suitable spawn position after 100 attempts.");
            return;
        }
    
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = spawnPosition;
        enemies.Add(newEnemy.transform);
        eSorter.enemies.Add(newEnemy.transform);
    }


    float MeasureDistance(Vector3 pos1, Vector3 pos2)
    {
        return (pos1-pos2).magnitude;
    }
}
