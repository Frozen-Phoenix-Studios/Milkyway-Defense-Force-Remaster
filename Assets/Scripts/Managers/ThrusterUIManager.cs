using System;
using FrozenPhoenixStudiosUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterUIManager : MonoSingleton<ThrusterUIManager>
{
    [SerializeField] private Slider _thrusterChargeBarSlider;
    [SerializeField] private TMP_Text _chargeAmountText;


    public void UpdateThrusterFillAmount(float amountRemaining)
    {
        _thrusterChargeBarSlider.value = amountRemaining;
        _chargeAmountText.SetText($"{(amountRemaining * 100).ToString("N0")}%");
    }
}