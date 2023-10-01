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
        // if (_mustApplyRopeForce)
        // {
        //     var v = _rope.BackEnd.transform.position - gameObject.transform.position;
        //     var dir = v.normalized;
        //
        //     transform.position += dir * (_ropeForce * Time.deltaTime);
        // }
        
        // Debug.Log(_currentMaxLength);
        // Debug.Log(_rope.AnchoringMode);
        
        if (_piece.Length >= _currentMaxLength && _rope.AnchoringMode == AnchoringMode.None)
        {
            _rope.AnchoringMode = AnchoringMode.ByBackEnd;
            //_currentMaxLength = _initialMaxLength;
            _mustApplyRopeForce = false;
        }
    }
}
