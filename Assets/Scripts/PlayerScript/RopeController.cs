using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrappingRopeLibrary.Enums;
using WrappingRopeLibrary.Scripts;

public class RopeController : MonoBehaviour
{
    [SerializeField] private Rope _rope;
    [SerializeField] private Piece _piece;

    [SerializeField] private float _ropeMaxLength = 10f;

    private float _currentMaxLength;
    
    private void Awake()
    {
        _rope.AnchoringMode = AnchoringMode.None;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece.Length >= _ropeMaxLength)
        {
            _rope.AnchoringMode = AnchoringMode.ByBackEnd;
            _currentMaxLength =
        }
    }
}
