using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider> OnEnter = null;
    [SerializeField] UnityEvent<Collider> OnExit = null;

    private void OnTriggerEnter(Collider _other)
    {
        OnEnterMethod(_other);
    }

    private void OnTriggerExit(Collider _other)
    {
        OnExitMethod(_other);
    }

    public virtual void OnEnterMethod(Collider _other)
    {
        OnEnter?.Invoke(_other);
    }

    public virtual void OnExitMethod(Collider _other)
    {
        OnExit?.Invoke(_other);
    }
}
