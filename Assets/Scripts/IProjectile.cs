using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float damage { get; set; }
    float dissipationTimer { get; set; }
    float hitDissipationValue { get; set; }
}
