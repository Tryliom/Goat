using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthBonus : BonusBase
{
   [SerializeField] private int _healAmount = 20;
    
    private PlayerStats _playerStatsRef;

    protected override void Awake()
    {
        base.Awake();
        _playerStatsRef = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    public override void BonusEffect()
    {
        _playerStatsRef.UpdateHealth(_healAmount);
    }

    protected override void Update()
    {
        base.Update();
    }
}
