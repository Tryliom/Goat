using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement")]
    private InputWrapper _inputs;
    private Rigidbody _rigidbody;
    private Vector3 _movementVelocity;
    private PlayerStats _stats;
    private Animator _animator;
    
    [SerializeField] private float _angularSpeed = 10f;
    
    private void Start()
    {
        _inputs = GetComponent<InputWrapper>();
        _rigidbody = GetComponent<Rigidbody>();
        _stats = GetComponent<PlayerStats>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        var inputValue = _inputs.move * _stats.movementSpeed;
        
        _movementVelocity = new Vector3(inputValue.x, 0, inputValue.y);
        _movementVelocity = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * _movementVelocity;
        _animator.SetFloat("Velocity", _movementVelocity.magnitude);
            
        var velocity = _rigidbody.velocity;
        
        velocity = new Vector3(velocity.x, 0, velocity.z).normalized;
        
        if (velocity.magnitude > 0.1f && _movementVelocity.normalized.magnitude > 0.1f)
        {
            var wantedRotation = Quaternion.LookRotation(velocity, Vector3.up);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, _angularSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        // Apply force to the rigidbody
        _rigidbody.velocity += _movementVelocity;

        // Limit the velocity
        if (_rigidbody.velocity.magnitude > _stats.maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _stats.maxSpeed;
        }
    }
}
