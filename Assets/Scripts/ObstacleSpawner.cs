using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;

    public float SpawnRate = 3f;
    private float time = 0f;

    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;

    void Start()
    {
        xMin = -transform.localScale.x * 4;
        xMax = transform.localScale.x * 4;
        zMin = -transform.localScale.z * 4;
        zMax = transform.localScale.z * 4;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > SpawnRate)
        {
            Obstacle obs = Instantiate(ObstaclePrefab, new Vector3(Random.Range(xMin, xMax), 0, Random.Range(zMin, zMax)), Quaternion.identity).GetComponent<Obstacle>();
            obs.StayTime = Random.Range(2f, 5f);
            obs.AppearTime = Random.Range(0.5f, 1.5f);
            time = 0f;
        }
    }
}
