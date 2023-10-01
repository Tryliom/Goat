using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WrappingRopeLibrary.Enums;

public class ExtendRopeBonus : BonusBase
{
    public static float RopeExtensionFactor = 2f;
    private RopeController _ropeContrRef;

    [SerializeField] private float _extendRopeDuration;

    protected override void Awake()
    {
        base.Awake();
        _ropeContrRef = FindObjectOfType<RopeController>();
    }

    public override void BonusEffect()
    {
        _ropeContrRef.Rope.AnchoringMode = AnchoringMode.None;
        
        _ropeContrRef.CurrentMaxLength *= RopeExtensionFactor;
        
        FindObjectOfType<PlayerStats>().ExtendRopeDuration += _extendRopeDuration;
        //Debug.Log(_ropeContrRef.CurrentMaxLength);
    }

    private void OnDestroy()
    {
        
    }
}
