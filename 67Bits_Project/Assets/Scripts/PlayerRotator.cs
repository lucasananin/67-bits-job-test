using UnityEngine;

public class PlayerRotator : AbstractRotator
{
    [SerializeField] CharacterController _cc = null;

    private void LateUpdate()
    {
        RotateToMovement(_cc.velocity);
    }
}
