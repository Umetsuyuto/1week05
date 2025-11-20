using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private RectTransform rect;
    private bool facingRight = true; // ‰Šú‚Í‰EŒü‚«

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        SetFacing(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SetFacing(false);
        else if (Input.GetKeyDown(KeyCode.D))
            SetFacing(true);
    }

    void SetFacing(bool toRight)
    {
        facingRight = toRight;
        rect.localScale = new Vector3(toRight ? 1 : -1, 1, 1);
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}
