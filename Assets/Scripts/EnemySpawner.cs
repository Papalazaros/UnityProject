using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnInterval = 1f;
    public Transform[] spawnPoints;
    public int maxEnemyCount;
    private float nextSpawnTime;
    private int enemyCount;
    private float? timeOfDay;

    private void Start()
    {
        GameEvents.instance.OnTimeOfDayChanged += (float timeOfDay) => this.timeOfDay = timeOfDay;
    }

    private void Update()
    {
        if (nextSpawnTime > 0) nextSpawnTime -= Time.deltaTime;

        if (nextSpawnTime <= 0 && enemyCount <= maxEnemyCount && (timeOfDay < 16 || timeOfDay < 6))
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
            Instantiate(enemy, randomPoint.position, Quaternion.identity);
            nextSpawnTime = spawnInterval;
            enemyCount++;
        }
    }

}
