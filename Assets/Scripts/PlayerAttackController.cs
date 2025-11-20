using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float speed = 800f; // UI 用
    public RectTransform leftLimit;
    public RectTransform rightLimit;

    private RectTransform rect;
    private int direction = 1; // 右向きは1、左向きは-1

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        // 移動
        rect.anchoredPosition += Vector2.right * speed * direction * Time.deltaTime;

        // 射程チェック
        if ((direction > 0 && rect.position.x >= rightLimit.position.x) ||
            (direction < 0 && rect.position.x <= leftLimit.position.x))
        {
            Destroy(gameObject);
            return;
        }

        // Enemy 当たり判定（自前判定）
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            RectTransform er = enemy.GetComponent<RectTransform>();
            if (Vector2.Distance(er.anchoredPosition, rect.anchoredPosition) < 40f) // 半径調整可
            {
                enemy.GetComponent<Enemy>()?.Die();
                Destroy(gameObject);
                break;
            }
        }
    }

    public void Initialize(bool facingRight)
    {
        direction = facingRight ? 1 : -1;
    }
}
