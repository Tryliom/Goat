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


    private void Start()
    {
        _inputs = GetComponent<InputWrapper>();
        _rb = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
        
        // Ignore collisions with his children
        foreach (Transform child in transform)
        {
            Physics.IgnoreCollision(child.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        _movementVelocity = _inputs.move * _stats.movementSpeed;
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
