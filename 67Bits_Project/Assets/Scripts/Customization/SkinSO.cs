using UnityEngine;

[CreateAssetMenu(fileName = "Skin_", menuName = "Scriptable Objects/Skin")]
public class SkinSO : ScriptableObject
{
    [SerializeField] Texture2D _texture = null;
    [SerializeField] int _price = 12;
    [SerializeField] bool _wasPurchased = false;

    public Texture2D Texture { get => _texture; }
    public int Price { get => _price; }
    public bool WasPurchased { get => _wasPurchased; set => _wasPurchased = value; }

    public void ResetRuntimeValues()
    {
        _wasPurchased = _price <= 0;
    }
}
