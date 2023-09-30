using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement")]
    private InputWrapper _inputs;
    private Rigidbody _rb;
    private Vector2 _movementVelocity;
    private PlayerStats _stats;


    void Start()
    {
        _inputs = GetComponent<InputWrapper>();
        _rb = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementVelocity = _inputs.move * _stats.movementSpeed;
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector3(_movementVelocity.x, 0, _movementVelocity.y);
    }
}
