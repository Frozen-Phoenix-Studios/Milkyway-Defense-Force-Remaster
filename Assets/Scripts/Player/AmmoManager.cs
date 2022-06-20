using System;
using System.Collections;
using UnityEngine;

public class AmmoManager : MonoBehaviour, IHaveAudio
{
    [SerializeField] private Stat _maxAmmo;
    private float _currentAmmo;
    private Coroutine _flashRoutine;
    [SerializeField] private AudioClip _noAmmoAudioClip;
    public AudioClip AudioClip => _noAmmoAudioClip;


    private void Start()
    {
        _currentAmmo = _maxAmmo.BaseValue;
        HudManager.Instance.UpdateAmmoText(_currentAmmo);
    }


    public bool UseAmmo()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo -= 1;
            HudManager.Instance.UpdateAmmoText(_currentAmmo);
            return true;
        }
        else
        {
            _flashRoutine ??= StartCoroutine(NoAmmoWarningRoutine());

            return false;
        }
    }
    
    private IEnumerator NoAmmoWarningRoutine()
    {
        PlayAudio();
        yield return HudManager.Instance.AmmoFlashRoutine();
        _flashRoutine = null;
    }


    public void PlayAudio()
    {
        AudioManager.Instance.PlayPlayerAttackAudioClip(this);
    }
}
