using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] Rigidbody2D _rb;
    public Rigidbody2D Rb { get { return _rb; } }

    [SerializeField] float _jumpBufferTime;
    public float JumpBufferTime { get { return _jumpBufferTime; } }
    public float JumpBuffer { get; set; }
    public bool IsJumpPressed { get; private set; } = false;
    public PlayerInputActions playerInputActions { get; private set; } 

    protected override void OnAwake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += JumpPerformed;
        playerInputActions.Player.Jump.canceled += JumpCanceled;
    }
    void JumpPerformed(InputAction.CallbackContext ctx)
    {
        IsJumpPressed = true;
    }
    void JumpCanceled(InputAction.CallbackContext ctx)
    {
        IsJumpPressed = false;
    }
}
