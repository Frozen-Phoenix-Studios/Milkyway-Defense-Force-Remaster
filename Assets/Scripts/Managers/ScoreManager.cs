
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreValueText;

    private void OnEnable()
    {
        EnemyBase.OnPointsAction += UpdateScore;
        Player.OnPointsAction += UpdateScore;
    }

    private void OnDisable()
    {
        EnemyBase.OnPointsAction -= UpdateScore;
        Player.OnPointsAction -= UpdateScore;
    }

    private void Start()
    {
        _score = 0;
        // _scoreValueText.SetText(_score.ToString());

        UpdateScore(0);
    }

    private void UpdateScore(int change)
    {
        // Debug.Log("Changing score");
        _score  += change;
        if (_score < 0)
            _score = 0;
        
        _scoreValueText.SetText(_score.ToString());

    }

}
