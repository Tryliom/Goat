using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrappingRopeLibrary.Enums;

public class ExtendRopeBonus : BonusBase
{
    [SerializeField] private float _ropeExtensionFactor = 2f;
    private RopeController _ropeContrRef;

    protected override void Awake()
    {
        base.Awake();
        _ropeContrRef = FindObjectOfType<RopeController>();
    }

    public override void BonusEffect()
    {
        _ropeContrRef.Rope.AnchoringMode = AnchoringMode.None;
        _ropeContrRef.CurrentMaxLength *= _ropeExtensionFactor;
        //Debug.Log(_ropeContrRef.CurrentMaxLength);
    }

    private void OnDestroy()
    {
        //_ropeContrRef.CurrentMaxLength /= _ropeExtensionFactor;
        _ropeContrRef.MustApplyRopeForce = true;
    }
}
