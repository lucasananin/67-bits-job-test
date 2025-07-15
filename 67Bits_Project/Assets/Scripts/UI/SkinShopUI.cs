using System.Collections.Generic;
using UnityEngine;

public class SkinShopUI : MonoBehaviour
{
    [SerializeField] SkinSO[] _skins = null;
    [SerializeField] Transform _content = null;
    [SerializeField] SkinSlotUI _slotPrefab = null;

    [Header("// READONLY")]
    [SerializeField] PlayerWallet _wallet = null;
    [SerializeField] PlayerSkinHandler _playerSkin = null;
    [SerializeField] List<SkinSlotUI> _slots = null;

    private void Start()
    {
        _wallet = FindFirstObjectByType<PlayerWallet>();
        _playerSkin = FindFirstObjectByType<PlayerSkinHandler>();

        int _count = _skins.Length;

        for (int i = 0; i < _count; i++)
        {
            _skins[i].ResetRuntimeValues();
            var _slot = Instantiate(_slotPrefab, _content);
            _slot.Init(this, _skins[i]);
            _slots.Add(_slot);
        }
    }

    public void TryBuy(SkinSO _skinSo)
    {
        if (_wallet.CurrentValue < _skinSo.Price)
        {
            return;
        }

        _wallet.DecreaseMoney(_skinSo.Price);
        _skinSo.WasPurchased = true;
        UpdateVisuals();
    }

    public void EquipSkin(SkinSO _skinSo)
    {
        _playerSkin.ChangeTexture(_skinSo.Texture);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        int _count = _slots.Count;

        for (int i = 0; i < _count; i++)
        {
            _slots[i].UpdateVisuals();
        }
    }

    internal bool IsEquipped(SkinSO _skinSo)
    {
        return _playerSkin.IsUsingTexture(_skinSo.Texture);
    }
}
