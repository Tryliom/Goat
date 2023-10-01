using System;
using Unity.VisualScripting;

public class BoomBonus : BonusBase
{
    public static event Action<BoomBonus> OnBoom;
    
    // Update is called once per frame
    public override void BonusEffect()
    {
        OnBoom?.Invoke(this);
        
        var projectiles = FindObjectsOfType<Projectile>();

        foreach (var proj in projectiles)
        {
            Destroy(proj.gameObject);
        }
    }
}
