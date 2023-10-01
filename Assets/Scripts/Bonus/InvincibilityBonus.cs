using System;
using Unity.VisualScripting;
using UnityEngine;

public class InvincibilityBonus : BonusBase
{
    [SerializeField] private float _invincibilityDuration = 3f;

    public static event Action OnInvicibility;
    
    public override void BonusEffect()
    {
        FindObjectOfType<PlayerStats>().InvincibilityDuration += _invincibilityDuration;
        
        OnInvicibility?.Invoke();
    }
}