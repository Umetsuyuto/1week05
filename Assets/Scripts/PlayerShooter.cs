using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private RectTransform shootPoint;
    [SerializeField] private RectTransform leftLimit;
    [SerializeField] private RectTransform rightLimit;
    [SerializeField] private PlayerController playerController;

    // 弾管理
    [SerializeField] private int maxAmmo = 5;
    private int currentAmmo = 5;

    // 弾UI（5個セット）
    [SerializeField] private Image[] bulletIcons;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 1f, 0.2f);

    [Header("SE設定")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;

    void Start()
    {
        UpdateBulletUI();
    }

    void Update()
    {
        // 射撃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryShoot();
        }

        // 向き変わった瞬間のリロード
        if (playerController.HasDirectionChanged())
        {
            Reload();
        }
    }

    void TryShoot()
    {
        if (currentAmmo <= 0) return;

        Shoot();

        currentAmmo--;
        UpdateBulletUI();

        // 射撃SE
        if (audioSource && shootClip)
            audioSource.PlayOneShot(shootClip);
    }

    void Shoot()
    {
        Transform canvasRoot = shootPoint.GetComponentInParent<Canvas>().transform;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, canvasRoot);

        var bc = bullet.GetComponent<PlayerAttackController>();
        bc.leftLimit = leftLimit;
        bc.rightLimit = rightLimit;

        bool facingRight = playerController.IsFacingRight();
        bc.Initialize(facingRight);
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateBulletUI();

        // リロードSE
        if (audioSource && reloadClip)
            audioSource.PlayOneShot(reloadClip);
    }

    void UpdateBulletUI()
    {
        for (int i = 0; i < bulletIcons.Length; i++)
        {
            bulletIcons[i].color = (i < currentAmmo) ? activeColor : emptyColor;
        }
    }
}
