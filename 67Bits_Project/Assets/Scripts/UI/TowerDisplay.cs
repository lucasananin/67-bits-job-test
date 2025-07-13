using TMPro;
using UnityEngine;

public class TowerDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    private void OnEnable()
    {
        TowerHandler.OnChanged += UpdateVisuals;
    }

    private void OnDisable()
    {
        TowerHandler.OnChanged -= UpdateVisuals;
    }

    private void UpdateVisuals(TowerHandler _tower)
    {
        _text.text = _tower.GetDisplayString();
    }
}
