using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{

    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesRemainingImage;
    
    public void OnEnable()
    {
        PlayerHealth.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int livesRemaining)
    {
        _livesRemainingImage.sprite = _livesSprites[livesRemaining];
    }

}
