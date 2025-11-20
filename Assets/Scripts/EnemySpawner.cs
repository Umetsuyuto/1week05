using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesRoot;

[SerializeField] private int maxEnemiesOnScreen = 10;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float speedUpAmount = 0.2f; // 基礎速度の上昇
    [SerializeField] private float baseEnemySpeed = 100f; // 基礎速度
    [SerializeField] private float randomAddSpeed = 40f;  // スポーン時のランダム追加

    [SerializeField] private bool spawnerOnLeft = true;

    [Header("SE")]
    [SerializeField] private AudioClip spawnFastSE;
    private AudioSource audioSource;
    private bool hasPlayedFastSE = false;

    private float timer = 0f;
    private int currentEnemies = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        // 基礎速度 + ランダム速度
        float spawnSpeed = baseEnemySpeed + Random.Range(0f, randomAddSpeed);
        e.Initialize(this, spawnSpeed, !spawnerOnLeft); // 右なら左向き、左なら右向き
        e.onDeath += HandleEnemyDeath;

        currentEnemies++;
    }

    private void HandleEnemyDeath()
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);

        // 基礎速度は毎回上昇
        baseEnemySpeed += speedUpAmount;

        // スポーン間隔の延長判定
        float chance = (spawnInterval <= 1f) ? 0.66f : 0.33f; // 1秒以下なら2/3
        if (Random.value < chance)
            spawnInterval += speedUpAmount; // 延長
        else
            spawnInterval -= speedUpAmount; // 短縮

        spawnInterval = Mathf.Clamp(spawnInterval, 0.3f, 6f);

        // 初めて1秒を切ったときにSE
        if (!hasPlayedFastSE && spawnInterval < 1f)
        {
            hasPlayedFastSE = true;
            if (audioSource != null && spawnFastSE != null)
                audioSource.PlayOneShot(spawnFastSE);
        }
    }
}

