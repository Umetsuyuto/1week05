using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RectTransform rect;
    private bool facingRight = true;

    // 向きが変わった瞬間のフラグ
    private bool directionChanged = false;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        SetFacing(true);
    }

    void Update()
    {
        directionChanged = false; // 毎フレーム初期化

        if (Input.GetKeyDown(KeyCode.A))
            SetFacing(false);
        else if (Input.GetKeyDown(KeyCode.D))
            SetFacing(true);
    }

    void SetFacing(bool toRight)
    {
        if (facingRight != toRight)
        {
            directionChanged = true; // 変化を検出
        }

        facingRight = toRight;
        rect.localScale = new Vector3(toRight ? 1 : -1, 1, 1);
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    // Shooter が参照する
    public bool HasDirectionChanged()
    {
        return directionChanged;
    }
}
