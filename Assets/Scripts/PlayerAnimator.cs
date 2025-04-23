using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator _Animator;
    [SerializeField] private PlayerLocomotionInput _PlayerLocomotionInput;      // called our playerlocomotion file here
    [SerializeField] private float LocomotionBlendInput = 2f;       // this variable is used to make smooth transition for character animation

    Vector2 CurrentBlendInput;

    public string inputX = "inputX";
    public string inputY = "inputY";

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        Vector2 _moveInput = _PlayerLocomotionInput._moveInput;     // here we are getting movement values from our playerlocomotion file 

        CurrentBlendInput = Vector2.Lerp(CurrentBlendInput, _moveInput, LocomotionBlendInput * Time.deltaTime); //it prevent animation to change instantly

        //_Animator.SetFloat(inputX, _moveInput.x);
        //_Animator.SetFloat(inputY, _moveInput.y);         //these provide values for controlling character animation


        _Animator.SetFloat(inputX, CurrentBlendInput.x);          // these are used to make animation smooth from one transition to other
        _Animator.SetFloat(inputY, CurrentBlendInput.y);


    }
}
