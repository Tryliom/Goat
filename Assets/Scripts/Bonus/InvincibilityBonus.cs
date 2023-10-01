using UnityEngine;

public class InvincibilityBonus : BonusBase
{
    [SerializeField] private float _invincibilityDuration = 3f;
    
    public override void BonusEffect()
    {
        FindObjectOfType<PlayerStats>().InvincibilityDuration += _invincibilityDuration;
    }
}