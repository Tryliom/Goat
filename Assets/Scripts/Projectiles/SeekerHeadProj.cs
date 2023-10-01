using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SeekerHeadProj : Projectile
{
    private Player _playerRef;
    
    protected override void Awake()
    {
        base.Awake();
        _playerRef = FindObjectOfType<Player>();
        _projectileType = ProjectileType.Bird;
    }

    protected override void Update()
    {
        base.Update();
        
        _targetPosition = _playerRef.transform.position;
        
        Vector3 selfToPlayer = _targetPosition - _transform.position;
        Vector3 direction = selfToPlayer.normalized;

        _trajectory = direction * _speed;

        transform.rotation = Quaternion.LookRotation(_trajectory);
    }

    protected override void Anim()
    {
        return;
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
