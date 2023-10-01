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

    [SerializeField] private float _ropeForce = 20;

    private bool _mustApplyRopeForce = false;

    private Piece[] _pieces;

    private float lengthResult;
    private float globalLength;
    
    private float _currentMaxLength;

    public bool MustApplyRopeForce
    {
        get => _mustApplyRopeForce;
        set => _mustApplyRopeForce = value;
    }

    public float CurrentMaxLength
    {
        get => _currentMaxLength;
        set => _currentMaxLength = value;
    }

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
        Piece.OnSpawn += AddPiece;
        
        _pieces = FindObjectsOfType<Piece>();
        
        _rope.AnchoringMode = AnchoringMode.None;
        _currentMaxLength = _initialMaxLength;
    }

    private void AddPiece(Piece p)
    {
        _pieces = FindObjectsOfType<Piece>();
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (var p in _pieces)
        // {
        //     lengthResult += p.Length;
        // }
        //
        // globalLength = lengthResult;
        //
        // lengthResult = 0;
        
        if (_piece.Length < _currentMaxLength)
        {
            //_rope.AnchoringMode = AnchoringMode.None;
            _mustApplyRopeForce = false;
        }
        else if (_piece.Length >= _currentMaxLength && !_mustApplyRopeForce)
        {
            _rope.AnchoringMode = AnchoringMode.ByBackEnd;
        }

        if (_mustApplyRopeForce)
        {
            _rope.AnchoringMode = AnchoringMode.None;
            var v = _rope.BackEnd.transform.position - gameObject.transform.position;
            var dir = v.normalized;
            
            transform.position += dir * (_ropeForce * Time.deltaTime);
        }
    }
}
