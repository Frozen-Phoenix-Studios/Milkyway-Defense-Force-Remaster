using System;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static event Action OnGameOver;
    [field: SerializeField] public bool IsGameOver { get; private set; } = false;

    private void Start()
    {
        IsGameOver = false;
    }

    public void SetGameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
        
    }
}
