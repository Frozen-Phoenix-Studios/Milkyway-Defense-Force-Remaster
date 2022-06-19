using UnityEngine;

public class Explosion : MonoBehaviour, IHaveAudio
{
    [SerializeField] private AudioClip _audioClip;
    public AudioClip AudioClip => _audioClip;

    [SerializeField] private bool IsEnemy;

    private void OnEnable() => PlayAudio();

    public void PlayAudio()
    {
        if (IsEnemy)
        {
            AudioManager.Instance.PlayEnemyExplosionAudioClip(this);
        }
        else
        {
            AudioManager.Instance.PlayPlayerExplosionAudioClip(this);
        }
    }

    private void FinishExplosion()
    {
        Destroy(gameObject);
    }
}
