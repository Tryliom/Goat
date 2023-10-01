using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WrappingRopeLibrary.Enums;
using WrappingRopeLibrary.Scripts;

public class RopeController : MonoBehaviour
{
    [SerializeField] private Rope _rope;
    [SerializeField] private Piece _piece;

    [SerializeField] private float _initialMaxLength = 5f;

    private float _currentMaxLength;

    public Rope Rope
    {
        get => _rope;
        set => _rope = value;
    }

    public Piece Piece
    {
        get => _piece;
        set => _piece = value;
    }

    private void Awake()
    {
        _rope.AnchoringMode = AnchoringMode.None;
        _currentMaxLength = _initialMaxLength;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece.Length >= _currentMaxLength && _rope.AnchoringMode == AnchoringMode.None)
        {
            _rope.AnchoringMode = AnchoringMode.ByBackEnd;
            _currentMaxLength = _initialMaxLength;
        }
    }
}
