using System;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    public static event Action<bool> OnGameOver;
    public static event Action OnGameStart;
    [field: SerializeField] public bool IsGameOver { get; private set; } = false;


    private void OnEnable()
    {
        PlayerInputReader.OnRestartPressed += Restart;
        OnGameOver?.Invoke(IsGameOver);
    }

    private void OnDisable()
    {
        PlayerInputReader.OnRestartPressed -= Restart;
        OnGameOver?.Invoke(IsGameOver);
    }

    public void SetGameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke(IsGameOver);
    }

    private void Restart()
    {
        if (IsGameOver)
            SceneManager.LoadScene("Game");
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void TriggerGameStart() => OnGameStart?.Invoke();
}
