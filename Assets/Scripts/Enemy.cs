using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner spawner;
    private RectTransform rect;
    private RectTransform player;
    private float moveSpeed;
    private bool facingRight;

    public System.Action onDeath;

    [SerializeField] private float damageDistance = 80f;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>();
    }

    public void Initialize(EnemySpawner spawner, float speed, bool facingRight)
    {
        this.spawner = spawner;
        this.moveSpeed = speed;
        this.facingRight = facingRight;

        // ç∂âEå¸Ç´îΩâf
        Vector3 scale = rect.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        rect.localScale = scale;
    }

    void Update()
    {
        MoveTowardPlayer();
        CheckDamage();
    }

    void MoveTowardPlayer()
    {
        if (player == null) return;
        Vector2 dir = (player.anchoredPosition - rect.anchoredPosition).normalized;
        rect.anchoredPosition += dir * moveSpeed * Time.deltaTime;
    }

    void CheckDamage()
    {
        if (Vector2.Distance(rect.anchoredPosition, player.anchoredPosition) < damageDistance)
        {
            var life = player.GetComponent<PlayerLifeManager>();
            if (life != null) life.TakeDamage();
            Die();
        }
    }

    public void OnHit()
    {
        Die();
    }

    public void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            OnHit();
            Destroy(collision.gameObject);
        }
    }
}

