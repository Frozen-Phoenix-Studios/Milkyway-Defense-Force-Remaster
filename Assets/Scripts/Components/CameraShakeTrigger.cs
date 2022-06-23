using System;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public static Action OnSmallShake;
    public static Action OnLargeShake;
    public static Action OnLongShake;

    public void TriggerSmallShake() => OnSmallShake?.Invoke();

    public void TriggerLargeShake() => OnLargeShake?.Invoke();

    public void TriggerLongShake() => OnLongShake?.Invoke();
}
