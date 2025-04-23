using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _CharacterController;      // calling chractercontroller class here
    [SerializeField] private PlayerLocomotionInput _PlayerLocomotionInput;      //calling our playerlocomotion file
    [SerializeField] private PlayerState _PlayerState;  //callin player state class
    [SerializeField] private Camera _Camera;        // calling our camera class here


    public float runAcceleration;
    public float runSpeed;
    public float drag;
    public float moveThreshold = 0.1f;

    public float lookSenseH;
    public float lookSenseV;
    public float lookLimitV;


    public Transform[] position = null;
    public Transform SpawnPosition;
    public Transform SpawnEnemyParent;
    public GameObject enemy;

    private Vector2 cameraRotation = Vector2.zero;
    private Vector2 playerTargetLocation = Vector2.zero;

    private void Awake() //this function is used to get referance of same object with which script is attached
    {
        _CharacterController = GetComponent<CharacterController>();
        _PlayerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _PlayerState = GetComponent<PlayerState>();
    }
    void Start()
    {
        GenerateGO();
    }

    void Update()
    {
        UpdatePlayerState();
        HandlePlayerMovement();
    }


    #region HandlePlayerMovement
    private void HandlePlayerMovement()
    {
        Vector3 cameraForwardXZ = new Vector3(_Camera.transform.forward.x, 0, _Camera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_Camera.transform.right.x, 0, _Camera.transform.right.z).normalized;

        Vector3 moveDirection = cameraRightXZ * _PlayerLocomotionInput._moveInput.x + cameraForwardXZ * _PlayerLocomotionInput._moveInput.y;
        Vector3 movementDelta = moveDirection * runAcceleration * Time.deltaTime;

        Vector3 newVelocity = _CharacterController.velocity + movementDelta;
        //_CharacterController.Move(newVelocity * Time.deltaTime);


        Vector3 currentDrag = newVelocity.normalized * drag;    // calculating character drag
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
    #endregion 

    #region LateUpdate
    private void LateUpdate()
    {
        cameraRotation.x += lookSenseH * _PlayerLocomotionInput._lookInput.x;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y - lookSenseV * _PlayerLocomotionInput._lookInput.y, -lookLimitV, lookLimitV);
        //this is used to rotate our character with curcor in Y-axis.


        playerTargetLocation.x += transform.eulerAngles.x + lookSenseH * _PlayerLocomotionInput._lookInput.x;
        transform.rotation = Quaternion.Euler(0f, playerTargetLocation.x, 0f);

        _Camera.transform.rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0f);


    }
    #endregion

    #region GenerateGO
    private void GenerateGO()
    {
        //Instantiate(enemy, SpawnPosition.position,Quaternion.identity);       this is used to spawn GameObject on a single position


        /* for(int i = 0; i< position.Length; i++)
         {
             Instantiate(enemy, position[i].position, Quaternion.identity, SpawnEnemyParent);    
             //it creates copy of Gameobject and place on given positions
         }*/
        //generating spawn GameObjects using for loop

        foreach (var pos in position)
        {
            Instantiate(enemy, pos.position, Quaternion.identity,SpawnEnemyParent);
        }
        //generating spawn GameObjects Using foreach loop
    }
    #endregion

    #region UpdatePlayerState
    private void UpdatePlayerState()
    {
        bool isMovementInput = _PlayerLocomotionInput._moveInput != Vector2.zero;
        bool isPlayerMoving = IsPlayerMoving();

        PlayerMovementState playerState;
        playerState = (isMovementInput || isPlayerMoving) ? PlayerMovementState.Running : PlayerMovementState.Idle;
        _PlayerState.UpdatePlayerState(playerState);

        /*if (isMovementInput || isPlayerMoving)
        {
            playerState = PlayerMovementState.Running;
        }
        else
        {
            playerState = PlayerMovementState.Idle;
        }*/
    }
    private bool IsPlayerMoving()
    {
        Vector3 playerVelocity = new Vector3(_CharacterController.velocity.x,0,_CharacterController.velocity.z);
        return playerVelocity.magnitude > moveThreshold;
    }
    #endregion

}
