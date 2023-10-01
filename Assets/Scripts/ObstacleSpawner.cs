using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;

    public float SpawnRate = 5f;
    private float time = 4f;
    private float spawnRateDecrementer = 0f;

    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;

    private void Start()
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
        spawnRateDecrementer += Time.deltaTime;
        if (spawnRateDecrementer >= 3f)
        {
            if (SpawnRate >= 1f)
            {
                SpawnRate -= 0.1f;
            }
            spawnRateDecrementer = 0f;
        }
        if (time > SpawnRate)
        {
            Obstacle obs = Instantiate(ObstaclePrefab, 
                new Vector3(Random.Range(xMin, xMax), 0.25f, Random.Range(zMin, zMax)), Quaternion.Euler(0, 0, 90f)).GetComponent<Obstacle>();
            obs.StayTime = Random.Range(2f, 5f);
            obs.AppearTime = Random.Range(0.5f, 1.5f);
            time = 0f;
        }

    }
}
