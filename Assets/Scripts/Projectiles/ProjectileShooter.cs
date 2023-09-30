using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    enum ProjectileType
    {
        Linear,
        Burst,
        SeekerHead,
        Count
    }

    [SerializeField] private LinearProj _linearProj;
    [SerializeField] private LinearProj _burstProj;
    [SerializeField] private SeekerHeadProj _seekerHeadProj;

    [SerializeField] private float _spawnFrequency = 2f;
    [SerializeField] private float _burstProjFrequency = 0.4f;

    [SerializeField] private int _burstProjNbr = 3;

    private float _elapsedTime;

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _spawnFrequency)
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