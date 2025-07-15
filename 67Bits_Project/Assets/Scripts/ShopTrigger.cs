using UnityEngine;

public class ShopTrigger : OnTriggerEvent
{
    [SerializeField] Canvas _canvas = null;

    private void Awake()
    {
        _canvas.enabled = false;
    }

    public override void OnEnterMethod(Collider _other)
    {
        _canvas.enabled = true;
        base.OnEnterMethod(_other);
    }

    public override void OnExitMethod(Collider _other)
    {
        _canvas.enabled = false;
        base.OnExitMethod(_other);
    }
}
