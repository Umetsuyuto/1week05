using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private int maxLife = 3;
    private int currentLife;

    void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage()
    {
        currentLife = Mathf.Max(0, currentLife - 1);

        Debug.Log("Player damaged! Life: " + currentLife);

        if (currentLife <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        // ƒV[ƒ“–¼‚ÍŒã‚Å•Ï‚¦‚Ä‚¢‚¢‚æ
        SceneManager.LoadScene("EndScene");
    }
}
