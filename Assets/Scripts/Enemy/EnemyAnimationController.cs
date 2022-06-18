using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _anim;
    private static readonly int Death = Animator.StringToHash("Death");

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("The animator is null on the Enemy Animation Controller");
    }

    public void SetDeathTrigger() => _anim.SetTrigger(Death);
}