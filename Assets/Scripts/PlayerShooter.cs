using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private RectTransform shootPoint;
    [SerializeField] private RectTransform leftLimit;
    [SerializeField] private RectTransform rightLimit;
    [SerializeField] private PlayerController playerController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Canvas íºâ∫Ç…ê∂ê¨ÇµÇƒ Player ÇÃîΩì]Ç…âeãøÇ≥ÇÍÇ»Ç¢
        Transform canvasRoot = shootPoint.GetComponentInParent<Canvas>().transform;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, canvasRoot);

        var bc = bullet.GetComponent<PlayerAttackController>();
        bc.leftLimit = leftLimit;
        bc.rightLimit = rightLimit;

        bool facingRight = playerController.IsFacingRight();
        bc.Initialize(facingRight);
    }
}
