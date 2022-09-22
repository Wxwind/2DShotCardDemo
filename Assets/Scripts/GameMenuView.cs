using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;
    private void Awake()
    {
        startGameBtn.onClick.AddListener(()=>{SceneManager.LoadScene(1);});
    }
}
