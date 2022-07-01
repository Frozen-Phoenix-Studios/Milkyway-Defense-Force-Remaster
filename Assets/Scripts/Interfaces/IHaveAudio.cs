using UnityEngine;

public interface IHaveAudio
{
    AudioType AudioType { get; }
    AudioClip AudioClip { get; }

    void PlayAudio();

}
