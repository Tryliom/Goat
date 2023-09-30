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
        xMin = -transform.localScale.x * 3;
        xMax = transform.localScale.x * 3;
        zMin = -transform.localScale.z * 3;
        zMax = transform.localScale.z * 3;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > SpawnRate)
        {
            Instantiate(ObstaclePrefab, new Vector3(Random.Range(xMin, xMax), 0, Random.Range(xMin, xMax)), Quaternion.identity);
            time = 0f;
        }
    }
}
