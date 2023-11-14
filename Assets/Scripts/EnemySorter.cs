using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySorter : MonoBehaviour
{
    public List<Transform> enemies;
    public Vector3 updatedTarget;
    private GameObject player;
    private List<Transform> closestTargets;
    private IPlayerAttack[] attacks;
    private Transform previousTarget;
    void Start()
    {
        enemies = GetComponent<EnemySpawner>().enemies;
        player = GameObject.FindGameObjectWithTag("Player");
        attacks = player.GetComponents<IPlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count <= 0) return;
        enemies.RemoveAll(enemy => enemy.Equals(null));
        if (enemies.Count <= 0) return;
        enemies.Sort((a, b) => (player.transform.position - a.position)
            .magnitude
            .CompareTo((player.transform.position - b.position).magnitude));
        updatedTarget = enemies.ElementAt(0).position;
        
    }
}
