using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator _Animator;
    [SerializeField] private PlayerLocomotionInput _PlayerLocomotionInput;
    public string inputX = "inputX";
    public string inputY = "inputY";

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        Vector2 _moveInput = _PlayerLocomotionInput._moveInput;
        _Animator.SetFloat(inputX, _moveInput.x);
        _Animator.SetFloat(inputY, _moveInput.y);
    }
}
