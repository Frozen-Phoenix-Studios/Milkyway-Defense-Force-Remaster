using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private Button _newGameButton; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
    }

    private void OnDisable()
    {
        _newGameButton.onClick.RemoveAllListeners();
    }

    private void StartNewGame()
    {
        GameStateManager.Instance.LoadNewGame();
    }

}
