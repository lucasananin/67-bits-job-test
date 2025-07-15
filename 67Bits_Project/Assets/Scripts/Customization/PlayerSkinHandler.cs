using UnityEngine;

public class PlayerSkinHandler : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] _renderers = null;

    public void ChangeTexture(Texture _texture)
    {
        int _count = _renderers.Length;

        for (int i = 0; i < _count; i++)
        {
            _renderers[i].material.mainTexture = _texture;
        }
    }

    public bool IsUsingTexture(Texture _texture)
    {
        return _renderers[0].material.mainTexture == _texture;
    }
}
