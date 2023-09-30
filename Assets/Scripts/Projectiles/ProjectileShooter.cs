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
    [SerializeField] private float _minSpawnFrequency = 1f;
    [SerializeField] private float _maxSpawnFrequency = 3f;
    [SerializeField] private float _spawnFrequencyDiminution;
    [SerializeField] private float _amountOfTimeToDiminuateFreq;
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_currentFrequency + " " + _lowerFrequency + " " + _higherFrequency);
        
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

            _currentFrequency = Random.Range(_lowerFrequency, _higherFrequency);
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