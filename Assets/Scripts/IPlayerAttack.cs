using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttack 
{
    float delay { get; set; }
    float damage { get; set; }
    int level { get; set; }
    Vector3 target { get; set; }
    Coroutine fireRoutine { get; set; }
    void LevelUp();
    IEnumerator Fire();
    
}
