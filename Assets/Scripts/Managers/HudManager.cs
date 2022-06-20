using System.Collections;
using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;

public class HudManager : MonoSingleton<HudManager>
{
    [SerializeField] private TMP_Text _ammoText;

    public IEnumerator AmmoFlashRoutine()
    {
        yield return UIUtilities.ObjectFlickerEffectConstant(_ammoText, 2.0f, 0.25f);
    }

    public void UpdateAmmoText(float currentAmmo)
    {
        _ammoText.SetText($"AMMO: {currentAmmo}");
    }
}
