using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagneticFieldUIManager : MonoSingleton<MagneticFieldUIManager>
{
    [SerializeField] private Slider _magneticFieldBatteryChargeBarSlider;
    [SerializeField] private TMP_Text _chargeAmountText;
    
    public void UpdateThrusterFillAmount(float amountRemaining)
    {
        _magneticFieldBatteryChargeBarSlider.value = amountRemaining;
        _chargeAmountText.SetText($"{(amountRemaining * 100).ToString("N0")}%");
    }
}