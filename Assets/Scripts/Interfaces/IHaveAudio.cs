using UnityEngine;

public interface IHaveAudio
{
    AudioClip AudioClip { get; }

    void PlayAudio();

}
