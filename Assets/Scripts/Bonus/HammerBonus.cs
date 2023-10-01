using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBonus : BonusBase
{
    public override void BonusEffect()
    {
        var pillars = FindObjectsOfType<Obstacle>();

        foreach (var p in pillars)
        {
            Destroy(p.gameObject);
        }
    }
}
