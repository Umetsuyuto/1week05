using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleSceneController : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("BaseScene"); // ƒQ[ƒ€‰æ–Ê‚ÌƒV[ƒ“–¼
    }
}
