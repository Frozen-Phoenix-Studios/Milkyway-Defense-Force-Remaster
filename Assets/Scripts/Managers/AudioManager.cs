using System;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _playerAttackAudioSource;
    [SerializeField] private AudioSource _playerExplosionAudioSource;
    [SerializeField] private AudioSource _playerEffectsAudioSource;
    
    [SerializeField] private AudioSource _enemyExplosionAudioSource;
    [SerializeField] private AudioSource _powerupAudioSource;
    [SerializeField] private AudioSource _enemyAttackAudioSource;

    public void PlayAudio(IHaveAudio audio)
    {
        AudioSource source = null;
        
        switch (audio.AudioType)
        {
            case AudioType.BackgroundMusic:
                break;
            case AudioType.PlayerAttack:
                source = _playerAttackAudioSource;
                break;
            case AudioType.PlayerEffect:
                source = _playerEffectsAudioSource;
                break;
            case AudioType.PlayerExplosion:
                source = _playerExplosionAudioSource;
                break;
            case AudioType.EnemyExplosion:
                source = _enemyExplosionAudioSource;
                break;
            case AudioType.EnemyAttack:
                source = _enemyAttackAudioSource;
                break;
            case AudioType.Powerup:
                source = _powerupAudioSource;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        source.clip = audio.AudioClip;
        source.Play();
    }

    
}