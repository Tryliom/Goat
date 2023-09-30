using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ProjectileShooter : MonoBehaviour
{
    enum ProjectileType
    {
        Linear,
        Burst,
        SeekerHead,
        Count
    }

    [Header("Projectiles Ref")]
    [SerializeField] private LinearProj _linearProj;
    [SerializeField] private LinearProj _burstProj;
    [SerializeField] private SeekerHeadProj _seekerHeadProj;

    [Header("Projectiles values")]
    [SerializeField] private float _burstProjFrequency = 0.4f;
    [SerializeField] private int _burstProjNbr = 3;
    
    [Header("Spawn Frequency")]
    [SerializeField] private float _baseMinSpawnFrequency = 5f;
    [SerializeField] private float _baseMaxSpawnFrequency = 7f;
    private float _minSpawnFrequency;
    private float _maxSpawnFrequency;
    private float _currentFrequency;
    [SerializeField] private float _spawnFrequencyDiminution;
    
    
    private float _elapsedTime;
    private float _totalTime;

    private void Awake()
    {
        _currentFrequency = Random.Range(_baseMinSpawnFrequency, _baseMaxSpawnFrequency);
        _minSpawnFrequency = _baseMinSpawnFrequency;
        _maxSpawnFrequency = _baseMaxSpawnFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_currentFrequency);
        
        _elapsedTime += Time.deltaTime;
        Debug.Log(Time.time);
        
        if (_elapsedTime >= _currentFrequency)
        {
            int projType = Random.Range(0, (int)ProjectileType.Count);

            switch (projType)
            {
                case (int)ProjectileType.Linear:
                    Instantiate(_linearProj.gameObject, transform.position, Quaternion.identity);
                    break;
                case (int)ProjectileType.Burst:
                    StartCoroutine(SpawnBurstProjectilesWithCooldown());
                    break;
                case (int)ProjectileType.SeekerHead:
                    Instantiate(_seekerHeadProj.gameObject, transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }

            _elapsedTime = 0f;

            _currentFrequency = Random.Range(_minSpawnFrequency, _maxSpawnFrequency);
        }
    }

    private IEnumerator SpawnBurstProjectilesWithCooldown()
    {
        for (int i = 0; i < _burstProjNbr; i++)
        {
            Instantiate(_burstProj.gameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_burstProjFrequency);
        }
    }
}