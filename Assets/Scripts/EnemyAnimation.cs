using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Image image;       // EnemyのImage
    [SerializeField] private Sprite[] sprites;   // 2枚のスプライト
    [SerializeField] private float interval = 0.2f;

    private int currentIndex = 0;

    void Start()
    {
        if (sprites.Length < 2)
        {
            Debug.LogWarning("EnemyAnimation: スプライトが2枚必要です");
            enabled = false;
            return;
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            image.sprite = sprites[currentIndex];
            currentIndex = (currentIndex + 1) % sprites.Length;
            yield return new WaitForSeconds(interval);
        }
    }
}
