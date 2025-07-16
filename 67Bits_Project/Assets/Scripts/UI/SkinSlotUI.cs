using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSlotUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _info = null;
    [SerializeField] TextMeshProUGUI _equippedText = null;
    [SerializeField] Button _buyButton = null;
    [SerializeField] Button _equipButton = null;

    [Header("// READONLY")]
    [SerializeField] SkinSO _so = null;
    [SerializeField] SkinShopUI _shopUI = null;

    internal void Init(SkinShopUI _shopUI, SkinSO _skinSO)
    {
        _so = _skinSO;
        this._shopUI = _shopUI;
        _buyButton.onClick.AddListener(() => _shopUI.TryBuy(_so));
        _equipButton.onClick.AddListener(() => _shopUI.EquipSkin(_so));
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (_so.WasPurchased)
        {
            _info.text = $"{_so.name}";
        }
        else
        {
            _info.text = $"{_so.name} ---------- R${_so.Price}";
        }

        _equippedText.enabled = _shopUI.IsEquipped(_so);
        _buyButton.gameObject.SetActive(!_so.WasPurchased);
        _equipButton.gameObject.SetActive(_so.WasPurchased);
    }
}
