using UnityEngine;

public class MultipleShotAttack : MonoBehaviour
{
    private int _childCount;

    private void Start()
    {
        //get component of each child and subscribe to event
        _childCount = transform.childCount;
        for (int i = 0; i < _childCount; i++)
        {
            var attack = transform.GetChild(i).GetComponent<Attack>();
            attack.OnDestroy += delegate(bool b) { AdjustChildCount(); };
        }
    }
    
    private void AdjustChildCount()
    {
        _childCount--;
        if (_childCount <= 0)
            Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}