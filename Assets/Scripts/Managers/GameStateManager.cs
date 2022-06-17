using System;
using System.Collections;
using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    public static event Action OnGameOver;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private string _gameOverString;
    
    [field: SerializeField] public bool IsGameOver { get; private set; } = false;

    private void OnEnable()
    {
        PlayerInputReader.OnRestartPressed += Restart;
    }

    private void OnDisable()
    {
        PlayerInputReader.OnRestartPressed -= Restart;

    }

    private void Start()
    {
        _gameOverPanel.SetActive(IsGameOver);
        _restartText.gameObject.SetActive(IsGameOver);

    }

    public void SetGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private void Restart()
    {
        if (IsGameOver)
            SceneManager.LoadScene("Game");
    }

    private IEnumerator GameOverRoutine()
    {
        IsGameOver = true;
        _gameOverPanel.SetActive(IsGameOver);
        OnGameOver?.Invoke();
        _restartText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
 
        
        while (IsGameOver)
        {
            yield return UIUtilities.TextTypeWriteEffect(_gameOverText, _gameOverString, 0.25f);
            yield return new WaitForSeconds(0.5f);
            yield return UIUtilities.ObjectFlickerEffectConstant(_gameOverText.gameObject, 3.0f, 0.85f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}