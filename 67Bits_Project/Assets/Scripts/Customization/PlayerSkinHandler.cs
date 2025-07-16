using UnityEngine;

public class PlayerSkinHandler : MonoBehaviour
{
    [SerializeField] SkinSO _defaultSkin = null;
    [SerializeField] SkinnedMeshRenderer[] _renderers = null;

    const string TEXTURE_PROPERTY = "_BaseMap";

    private void Awake()
    {
        ChangeTexture(_defaultSkin.Texture);
    }

    public void ChangeTexture(Texture _texture)
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
        {
            var _renderer = _renderers[i];
            var _block = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_block);
            _block.SetTexture(TEXTURE_PROPERTY, _texture);
            _renderer.SetPropertyBlock(_block);
        }
    }

    public bool IsUsingTexture(Texture _texture)
    {
        var _renderer = _renderers[0];
        var _block = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_block);
        return _block.GetTexture(TEXTURE_PROPERTY) == _texture;
    }
}
