using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _CharacterController;
    [SerializeField] private PlayerLocomotionInput _PlayerLocomotionInput;
    [SerializeField] private Camera _Camera;


    public float runAcceleration;
    public float runSpeed;
    public float drag;

    public float lookSenseH;
    public float lookSenseV;
    public float lookLimitV;

    private Vector2 cameraRotation = Vector2.zero;
    private Vector2 playerTargetLocation = Vector2.zero;
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
        //_CharacterController.Move(newVelocity * Time.deltaTime);


        Vector3 currentDrag = newVelocity.normalized * drag;
        if (newVelocity.magnitude > drag)
        {
            newVelocity = newVelocity - currentDrag;
        }
        else
        {
            newVelocity = Vector3.zero;
        }
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);

        _CharacterController.Move(newVelocity * Time.deltaTime);// move the character
    }
    private void LateUpdate()
    {
        cameraRotation.x += lookSenseH * _PlayerLocomotionInput._lookInput.x;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y - lookSenseV * _PlayerLocomotionInput._lookInput.y, -lookLimitV, lookLimitV);

        playerTargetLocation.x += transform.eulerAngles.x + lookSenseH * _PlayerLocomotionInput._lookInput.x;
        transform.rotation = Quaternion.Euler(0f, playerTargetLocation.x, 0f);

        _Camera.transform.rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0f);
    }
}
