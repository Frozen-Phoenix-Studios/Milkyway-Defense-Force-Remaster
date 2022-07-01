using System;
using System.Collections;
using UnityEngine;

public class AmmoManager : MonoBehaviour, IHaveAudio, ISuppliable
{
    [SerializeField] private SupplyType _supplyType;
    public SupplyType SupplyType => _supplyType;

    [SerializeField] private Stat _maxAmmo;
    private float _currentAmmo;
    private Coroutine _flashRoutine;
    [SerializeField] private AudioClip _noAmmoAudioClip;
    public AudioClip AudioClip => _noAmmoAudioClip;
    [SerializeField] private AudioType _audioType;
    public AudioType AudioType => _audioType;

    private void Start()
    {
        _currentAmmo = _maxAmmo.BaseValue;
        HudManager.Instance.SetMaxAmmo(_maxAmmo.BaseValue);
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

        _flashRoutine ??= StartCoroutine(NoAmmoWarningRoutine());

        return false;
    }

    private IEnumerator NoAmmoWarningRoutine()
    {
        PlayAudio();
        yield return HudManager.Instance.AmmoFlashRoutine();
        _flashRoutine = null;
    }

    public void Resupply(float amount)
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo += amount, 0, _maxAmmo.BaseValue);
        HudManager.Instance.UpdateAmmoText(_currentAmmo);
    }

    public void PlayAudio()
    {
        // AudioManager.Instance.PlayPlayerAttackAudioClip(this);
        AudioManager.Instance.PlayAudio(this);
    }

}