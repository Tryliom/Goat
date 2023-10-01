using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrappingRopeLibrary.Enums;

public class ExtendRopeBonus : BonusBase
{
    private RopeController _ropeContrRef;

    protected override void Awake()
    {
        base.Awake();
        _ropeContrRef = FindObjectOfType<RopeController>();
    }

    public override void BonusEffect()
    {
        _ropeContrRef.Rope.AnchoringMode = AnchoringMode.None;
    }
}
