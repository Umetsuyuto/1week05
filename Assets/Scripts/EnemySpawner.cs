using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesRoot;

    [SerializeField] private int maxEnemiesOnScreen = 10;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float speedUpAmount = 0.2f;

    [SerializeField] private float enemyMoveSpeed = 100f;
    [SerializeField] private bool spawnerOnLeft = true; // 右から出るかどうか

    private float timer = 0f;
    private int currentEnemies = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemiesOnScreen) return;

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemiesRoot);
        Enemy e = enemy.GetComponent<Enemy>();
        e.Initialize(this, enemyMoveSpeed, !spawnerOnLeft); // 右からなら左向き、左からなら右向き
        e.onDeath += HandleEnemyDeath;

        currentEnemies++;
    }

    private void HandleEnemyDeath()
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        SpeedUp();
    }

    public void SpeedUp()
    {
        spawnInterval = Mathf.Max(0.3f, spawnInterval - speedUpAmount);
    }
}

