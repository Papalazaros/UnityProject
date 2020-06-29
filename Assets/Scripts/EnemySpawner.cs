using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemy;
    public float spawnInterval = 1f;
    public Transform[] spawnPoints;
    private float _nextSpawnTime;

    private void Update()
    {
        _nextSpawnTime -= Time.deltaTime;

        if (_nextSpawnTime <= 0)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
            Instantiate(_enemy, randomPoint.position, Quaternion.identity);
            _nextSpawnTime = spawnInterval;
        }
    }

}
