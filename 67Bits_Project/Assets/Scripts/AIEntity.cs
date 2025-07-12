using UnityEngine;

public class AIEntity : MonoBehaviour
{
    [SerializeField] Rigidbody _handleRB = null;

    public Rigidbody HandleRB { get => _handleRB; }

    public void GoToTower()
    {
        _handleRB.isKinematic = true;
    }
}
