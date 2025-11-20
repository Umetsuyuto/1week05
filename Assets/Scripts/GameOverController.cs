using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void OnClick()
    {
        SceneManager.LoadScene(SceneName);   
    }
}
