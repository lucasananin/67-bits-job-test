using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TowerUpgradeSO", menuName = "Scriptable Objects/Tower Upgrade")]
public class TowerUpgradeSO : ScriptableObject
{
    [SerializeField] List<UpgradeData> _list = null;

    [Header("// READONLY")]
    [SerializeField] int _currentLevel = 0;

    public event UnityAction OnChanged = null;

    public void IncreaseLevel()
    {
        _currentLevel++;
        OnChanged?.Invoke();
    }

    public int GetNextLevelCost()
    {
        return _list[_currentLevel].nextLevelCost;
    }

    public int GetCurrentCapacity()
    {
        return _list[_currentLevel].capacity;
    }

    internal bool HasUpgradeAvailable()
    {
        return _currentLevel < _list.Count - 1;
    }

    public int GetCapacityDifference()
    {
        return Mathf.Abs(_list[_currentLevel].capacity - _list[_currentLevel + 1].capacity);
    }

    internal void ResetRuntimeValues()
    {
        _currentLevel = 0;
    }

    [System.Serializable]
    public class UpgradeData
    {
        public int capacity = 3;
        public int nextLevelCost = 30;
    }
}
