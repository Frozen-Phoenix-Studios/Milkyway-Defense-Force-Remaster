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

    public void PlayPlayerAttackAudioClip(IHaveAudio audio)
    {
        if (audio == null)
            return;
        
        _playerAttackAudioSource.clip = audio.AudioClip;
        _playerAttackAudioSource.Play();
    }

    public void PlayEnemyExplosionAudioClip(IHaveAudio audio)
    {
        if (audio == null)
            return;
        
        _enemyExplosionAudioSource.clip = audio.AudioClip;
        _enemyExplosionAudioSource.Play();
    }

    public void PlayPlayerExplosionAudioClip(IHaveAudio audio)
    {
        if (audio == null)
            return;
        
        _playerExplosionAudioSource.clip = audio.AudioClip;
        _playerExplosionAudioSource.Play();
    }

    public void PlayPowerupAudioClip(IHaveAudio audio)
    {
        if (audio == null)
            return;
        
        _powerupAudioSource.clip = audio.AudioClip;
        _powerupAudioSource.Play();
    }

    public void EnemyAttackAudioClip(IHaveAudio audio)
    {
        if (audio == null)
            return;
        
        _enemyAttackAudioSource.clip = audio.AudioClip;
        _enemyAttackAudioSource.Play();
    }
    
    public void PlayPlayerEffectAudioClip(IHaveAudio audio)
    {
        if (audio == null || _playerEffectsAudioSource.isPlaying)
            return;
        
        _playerEffectsAudioSource.clip = audio.AudioClip;
        _playerEffectsAudioSource.Play();
    }
}