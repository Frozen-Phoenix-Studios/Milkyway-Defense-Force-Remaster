using FrozenPhoenixStudiosUtilities;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _playerAttackAudioSource;
    [SerializeField] private AudioSource _enemyExplosionAudioSource;
    [SerializeField] private AudioSource _playerExplosionAudioSource;

    public void PlayPlayerAttackAudioClip(IHaveAudio audio)
    {
        _playerAttackAudioSource.clip = audio.AudioClip;
        _playerAttackAudioSource.Play();
    }

    public void PlayEnemyExplosionAudioClip(IHaveAudio audio)
    {
        _enemyExplosionAudioSource.clip = audio.AudioClip;
        _enemyExplosionAudioSource.Play();
    }

    public void PlayPlayerExplosionAudioClip(IHaveAudio audio)
    {
        _playerExplosionAudioSource.clip = audio.AudioClip;
        _playerExplosionAudioSource.Play();
    }
}