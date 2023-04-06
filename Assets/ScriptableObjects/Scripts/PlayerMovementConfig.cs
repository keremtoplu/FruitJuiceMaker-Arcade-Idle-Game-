using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Config/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    public float MovementSpeed;
    public float RotationSpeed;
}