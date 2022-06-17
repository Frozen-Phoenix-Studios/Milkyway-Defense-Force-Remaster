using UnityEngine;

[CreateAssetMenu(menuName = "Attachable")]
public class Attachable : ScriptableObject
{
    public GameObject AttachablePrefab;
    public IAttachable IAttachable;
    public float ActiveTime;


    private void OnValidate()
    {
        IAttachable = AttachablePrefab.GetComponent<IAttachable>();
        if (IAttachable == null)
            Debug.LogError("The IAttachable is null");
    }
}