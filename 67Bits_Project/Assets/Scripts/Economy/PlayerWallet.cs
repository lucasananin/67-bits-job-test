using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] int _currentValue = 0;

    public int CurrentValue { get => _currentValue; }

    public static event UnityAction<int> OnChanged = null;

    private void Start()
    {
        OnChanged?.Invoke(_currentValue);
    }

    private void OnEnable()
    {
        DropArea.OnDelivered += IncreaseMoney;
    }

    private void OnDisable()
    {
        DropArea.OnDelivered -= IncreaseMoney;
    }

    private void IncreaseMoney(AIEntity _entity)
    {
        _currentValue += _entity.GetCostValue();
        OnChanged?.Invoke(_currentValue);
    }

    public void DecreaseMoney(int _value)
    {
        _currentValue -= _value;
        OnChanged?.Invoke(_currentValue);
    }
}
