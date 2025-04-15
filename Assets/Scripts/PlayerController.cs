using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private PlayerLocomotionInput _PlayerLocomotionInput;
    [SerializeField] private Camera _Camera;


    public float runAcceleration;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(_Camera.transform.forward.x,0,_Camera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_Camera.transform.right.x, 0, _Camera.transform.right.z).normalized;

        Vector3 moveDirection = cameraRightXZ * _PlayerLocomotionInput._moveInput.x + cameraForwardXZ * _PlayerLocomotionInput._moveInput.y;
        Vector3 movementDelta = moveDirection * runAcceleration * Time.deltaTime;

        Vector3 newVelocity = _CharacterController.velocity + movementDelta;
        _CharacterController.Move(newVelocity * Time.deltaTime);
    }
}
