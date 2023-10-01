using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ProjectileShooter : MonoBehaviour
{
    private enum ProjectileType
    {
        Linear,
        Burst,
        SeekerHead,
        Count
    }

    [Header("Projectiles Ref")]
    [SerializeField] private LinearProj _linearProj;

    [SerializeField] private List<LinearProj> _burstProjectiles;
    
    // [SerializeField] private LinearProj _burstProjBurger;
    // [SerializeField] private LinearProj _burstProjCoke;
    // [SerializeField] private LinearProj _burstProjFries;
    [SerializeField] private SeekerHeadProj _seekerHeadProj;

    [Header("Projectiles values")]
    [SerializeField] private float _burstProjFrequency = 0.4f;
    [SerializeField] private int _burstProjNbr = 3;
    
    [Header("Spawn Frequency")]
    [SerializeField] private float _baseMinSpawnFrequency = 5f;
    [SerializeField] private float _baseMaxSpawnFrequency = 7f;
    [SerializeField] private float _minSpawnFrequency = 1f;
    [SerializeField] private float _maxSpawnFrequency = 3f;
    [SerializeField] private float _spawnFrequencyDiminution;
    [SerializeField] private float _amountOfTimeToDiminuateFreq;

    [Header("Enemy")] 
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnRadius = 20f;
    [SerializeField] private float _attackRadius = 6f;
    [SerializeField] private Transform _center;
    [SerializeField] private float _ignoreAngle = 50f;
    
    private float _lowerFrequency;
    private float _higherFrequency;
    private float _currentFrequency;

    private bool MustDiminuateFrequency =>
        _lowerFrequency > _minSpawnFrequency && _higherFrequency > _maxSpawnFrequency; 
    
    private float _elapsedTime;
    private float _diminTime;

    private void Awake()
    {
        _currentFrequency = Random.Range(_baseMinSpawnFrequency, _baseMaxSpawnFrequency);
        _lowerFrequency = _baseMinSpawnFrequency;
        _higherFrequency = _baseMaxSpawnFrequency;
    }
    
    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _diminTime += Time.deltaTime;

        if (_diminTime >= _amountOfTimeToDiminuateFreq && MustDiminuateFrequency)
        {
            _lowerFrequency -= _spawnFrequencyDiminution;
            _higherFrequency -= _spawnFrequencyDiminution;

            _diminTime = 0;
        }

        if (_lowerFrequency < _minSpawnFrequency || _higherFrequency < _maxSpawnFrequency)
        {
            _lowerFrequency = _minSpawnFrequency;
            _higherFrequency = _maxSpawnFrequency;
        }
        
        if (_elapsedTime >= _currentFrequency)
        {
            // Spawn an enemy far away at _spawnRadius
            var farPosition = GetRandomPositionAroundCenter();
            var targetPosition = Vector3.Lerp(farPosition, _center.position, (_spawnRadius - _attackRadius) / _spawnRadius);
            var enemy = Instantiate(_enemyPrefab.gameObject, farPosition, Quaternion.identity).GetComponent<Enemy>();
            
            enemy.Init(farPosition, targetPosition);
            enemy.SetOnReachedTarget(ShootFromEnemy);

            _elapsedTime = 0f;

            _currentFrequency = Random.Range(_lowerFrequency, _higherFrequency);
        }
    }

    private Vector3 GetRandomPositionAroundCenter()
    {
        var x = Random.Range(_spawnRadius, _spawnRadius + 2);
        var z = Random.Range(_spawnRadius, _spawnRadius + 2);
        
        // Use a random angle between 0 and 360 degrees converted to radians.
        var angle = (Random.Range(_ignoreAngle, 360) + _ignoreAngle) * Mathf.Deg2Rad;
        
        // Get the X & Z position of the unit circle using the random angle.
        x *= Mathf.Cos(angle);
        z *= Mathf.Sin(angle);
        

        return new Vector3(x, 0, z) + _center.position;
    }
    
    private void ShootFromEnemy(Enemy enemy)
    {
        var projType = Random.Range(0, (int) ProjectileType.Count);

        switch (projType)
        {
            case (int) ProjectileType.Linear:
                Instantiate(_linearProj.gameObject, enemy.transform.position, Quaternion.identity);
                enemy.GoBack();
                break;
            case (int) ProjectileType.Burst:
                StartCoroutine(SpawnBurstProjectilesWithCooldown(enemy));
                break;
            case (int) ProjectileType.SeekerHead:
                Instantiate(_seekerHeadProj.gameObject, enemy.transform.position, Quaternion.identity);
                enemy.GoBack();
                break;
            default:
                break;
        }
    }
    
    private IEnumerator SpawnBurstProjectilesWithCooldown(Enemy enemy)
    {
        for (var i = 0; i < _burstProjNbr; i++)
        {
            Instantiate(_burstProjectiles[i], enemy.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_burstProjFrequency);
        }
        
        enemy.GoBack();
    }
}
