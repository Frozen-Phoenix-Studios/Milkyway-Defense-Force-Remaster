using System.Collections;
using TMPro;
using UnityEngine;

public class WaveUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveIndexText;
    private int _currentWave = 0;

    [SerializeField] private TMP_Text _nextWaveCountDownText;
    [SerializeField] private GameObject _nextWaveCountDownContainer;
    [SerializeField] private GameObject _waveTextContainer;

    private void Start()
    {
        _waveTextContainer.SetActive(false);
        _currentWave = 0;
        SpawnManager.Instance.OnWaveComplete += StartCountdown;
        GameStateManager.OnGameStart += ShowWaveText;
    }

    private void ShowWaveText()
    {
        _currentWave++;
        _waveIndexText.SetText($"{_currentWave}");
        _waveTextContainer.SetActive(true);
    }

    private void OnDisable()
    {
        SpawnManager.Instance.OnWaveComplete -= StartCountdown;
        GameStateManager.OnGameStart -= ShowWaveText;

    }

    private void StartCountdown(float length)
    {
        StartCoroutine(StartCountDown(length));
    }


    private IEnumerator StartCountDown(float countDownLength)
    {
        _nextWaveCountDownContainer.SetActive(true);
        while (countDownLength > 0)
        {
            countDownLength -= Time.deltaTime;
            _nextWaveCountDownText.SetText(countDownLength.ToString("N1"));
            yield return new WaitForEndOfFrame();
        }
        _currentWave++;
        _waveIndexText.SetText($"{_currentWave}");
        _nextWaveCountDownContainer.SetActive(false);
    }
}