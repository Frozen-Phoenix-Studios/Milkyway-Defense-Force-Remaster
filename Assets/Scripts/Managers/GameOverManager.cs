using System.Collections;
using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoSingleton<GameOverManager>
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private string _gameOverString;

    private bool _isGameOver;
    
    private void OnEnable() => GameStateManager.OnGameOver += SetGameOverState;

    private void OnDisable() => GameStateManager.OnGameOver -= SetGameOverState;

    private void SetGameOverState(bool gameOverState)
    {
        _isGameOver = gameOverState;
        if (gameOverState)
            StartCoroutine(GameOverRoutine());
    }

    private void Start()
    {
        _gameOverPanel.SetActive(_isGameOver);
        _restartText.gameObject.SetActive(_isGameOver);
    }

    private IEnumerator GameOverRoutine()
    {
        _gameOverPanel.SetActive(_isGameOver);
        _restartText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
 
        while (_isGameOver)
        {
            yield return UIUtilities.TextTypeWriteEffect(_gameOverText, _gameOverString, 0.25f);
            yield return new WaitForSeconds(0.5f);
            yield return UIUtilities.ObjectFlickerEffectConstant(_gameOverText.gameObject, 3.0f, 0.85f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}