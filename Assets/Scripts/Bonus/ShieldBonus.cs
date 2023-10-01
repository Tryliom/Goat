using UnityEngine;

public class ShieldBonus : BonusBase
{
    public override void BonusEffect()
    {
        FindObjectOfType<PlayerStats>().ShieldCount++;
    }
}