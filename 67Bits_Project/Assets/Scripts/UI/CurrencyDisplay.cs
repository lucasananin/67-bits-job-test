using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    private void OnEnable()
    {
        PlayerWallet.OnChanged += UpdateVisuals;
    }

    private void OnDisable()
    {
        PlayerWallet.OnChanged -= UpdateVisuals;
    }

    private void UpdateVisuals(int _newValue)
    {
        _text.text = $"R$ {_newValue}";
    }
}
