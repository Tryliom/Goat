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
    private Animator _animator;


    private void Start()
    {
        _inputs = GetComponent<InputWrapper>();
        _rb = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        _movementVelocity = _inputs.move * _stats.movementSpeed;
        _animator.SetFloat("Velocity", _movementVelocity.magnitude);

        Vector3 playerDirection = Vector3.right * _movementVelocity.x + Vector3.forward * _movementVelocity.y;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        // Apply force to the rigidbody
        _rb.velocity += new Vector3(_movementVelocity.x, 0, _movementVelocity.y);

        // Limit the velocity
        if (_rb.velocity.magnitude > _stats.movementSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _stats.movementSpeed;
        }
    }
}
