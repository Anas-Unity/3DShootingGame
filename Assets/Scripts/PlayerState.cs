using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerMovementState currentPlayerMovementState = PlayerMovementState.Idle;

    public void UpdatePlayerState(PlayerMovementState playerState)
    {
        currentPlayerMovementState = playerState;
    }

}

public enum PlayerMovementState
{
    Idle,
    Running,
    Jump,
    Falling
}