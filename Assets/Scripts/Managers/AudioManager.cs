using System.Collections;
using System.Collections.Generic;
using FrozenPhoenixStudiosUtilities;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _playerAttackAudioSource;

    public void PlayPlayerAttackAudioClip(IHaveAudio audio)
    {
        _playerAttackAudioSource.clip = audio.AudioClip;
        _playerAttackAudioSource.Play();
    }
}
