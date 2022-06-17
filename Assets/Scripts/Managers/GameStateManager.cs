using System;
using System.Collections;
using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    public static event Action OnGameOver;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private string _gameOverString;

    [field: SerializeField] public bool IsGameOver { get; private set; } = false;

    private void Start()
    {
        _gameOverPanel.SetActive(IsGameOver);
    }

    public void SetGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        IsGameOver = true;
        _gameOverPanel.SetActive(IsGameOver);
        OnGameOver?.Invoke();
        
        while (IsGameOver)
        {
            yield return new WaitForSeconds(0.25f);
            yield return UIUtilities.TextTypeWriteEffect(_gameOverText, _gameOverString, 0.25f);
            yield return UIUtilities.ObjectFlickerEffectConstant(_gameOverText.gameObject, 3.0f, 0.35f);
        }
    }
}