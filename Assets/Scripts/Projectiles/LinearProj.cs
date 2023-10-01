using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This Projectile get the player position on Awake() and goes straight in this direction.
/// </summary>

public class LinearProj : Projectile
{
    protected override void Awake()
    {
        base.Awake();
        
        _targetPosition = FindObjectOfType<Player>().transform.position;
        
        Vector3 selfToPlayer = _targetPosition - _transform.position;
        Vector3 direction = selfToPlayer.normalized;

        _trajectory = direction * _speed;
        
        _projectileType = ProjectileType.Chair;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // protected override void OnTriggerEnter(Collider other)
    // {
    //     base.OnTriggerEnter(other);
    // }
}
