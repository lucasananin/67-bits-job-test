using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    private float _deltaTime = 0f;
    private float _msec = 0f;
    private float _fps = 0f;

    private void Awake()
    {
        _text.raycastTarget = false;
    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        _msec = _deltaTime * 1000.0f;
        _fps = 1.0f / _deltaTime;
        _text.SetText("{0:0.0} ms ({1:0.} fps)", _msec, _fps);
    }
}
