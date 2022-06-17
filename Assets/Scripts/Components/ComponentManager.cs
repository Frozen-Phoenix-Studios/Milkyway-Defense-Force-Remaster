using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _attachablesList = new List<GameObject>();
    
    private void OnValidate()
    {
        _attachablesList.Clear();
        var myTransform = transform;
        for (int i = 0; i < myTransform.childCount; i++)
        {
            var child = myTransform.GetChild(i);
            var attachable = child.GetComponent<IAttachable>();
            if (attachable != null)
                _attachablesList.Add(child.gameObject);
        }
    }

    private void Start()
    {
        foreach (var attachable in _attachablesList)
        {
            Debug.Log(attachable);
        }
    }

    public void Attach(Attachable attachable)
    {
        var component = _attachablesList.First(t => t.name == attachable.AttachablePrefab.name).GetComponent<IAttachable>();
        if (component != null)
        {
            Debug.Log("Found attachable");
            if (attachable.ActiveTime == 0)
                component.Attach();
        }
        else
        {
            Debug.Log($"Attachable not found {attachable.IAttachable}");
        }
    }
}