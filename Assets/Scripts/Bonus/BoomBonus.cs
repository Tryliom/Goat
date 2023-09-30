using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBonus : BonusBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void BonusEffect()
    {
        var projectiles = FindObjectsOfType<Projectile>();

        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
