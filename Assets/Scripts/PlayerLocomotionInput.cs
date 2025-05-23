using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerActions
{

    public PlayerControls _playerControls;
    public Vector2 _moveInput;
    public Vector2 _lookInput;


    private void OnEnable()
    {

        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _playerControls.Player.Enable();
        _playerControls.Player.SetCallbacks(this);

    }

    private void OnDisable()
    {

        _playerControls.Disable();
        _playerControls.Player.Disable();
        _playerControls.Player.RemoveCallbacks(this);

    }

    #region Controls
    public void OnAttack(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();      // this is used to get character values when we move our cursor 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();      // tis is used to get character values when moving
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }
    #endregion

}
