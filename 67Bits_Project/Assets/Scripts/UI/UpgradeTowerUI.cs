using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerUI : MonoBehaviour
{
    [SerializeField] TowerUpgradeSO _so = null;
    [SerializeField] TextMeshProUGUI _text = null;
    [SerializeField] Button _button = null;

    [Header("// READONLY")]
    [SerializeField] PlayerWallet _wallet = null;

    private void Start()
    {
        _wallet = FindFirstObjectByType<PlayerWallet>();
        UpdateVisuals();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryBuy);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryBuy);
    }

    private void TryBuy()
    {
        var _nextLevelCost = _so.GetNextLevelCost();

        if (_wallet.CurrentValue < _nextLevelCost)
        {
            return;
        }

        _wallet.DecreaseMoney(_nextLevelCost);
        _so.IncreaseLevel();
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        var _nextLevelCost = _so.GetNextLevelCost();

        if (_so.HasUpgradeAvailable())
        {
            _text.text = $"+{_so.GetCapacityDifference()}\nR$ {_nextLevelCost}";
            _button.interactable = true;
        }
        else
        {
            _text.text = $"Max Level";
            _button.interactable = false;
        }
    }
}
