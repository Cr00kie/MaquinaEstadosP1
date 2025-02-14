using UnityEngine;

public class PlayerJumpingState : BaseState
{
    [SerializeField] float _jumpStrength;
    bool _isGrounded;
    float _jumpBuffer;
    bool _airSpeedReduced;
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetCTX<PlayerStateMachine>().Rb;
    }

    public override void CheckSwitchState()
    {
        if (_isGrounded) ChangeState(Ctx.GetStateByType<IsGroundedState>());
    }

    public override void EnterState()
    {
        SetSubState(Ctx.GetStateByType<PlayerIdleState>());
        print("Entering jumping state...");
        _isGrounded = false;
        _rb.velocity += new Vector2(0, _jumpStrength);
        _airSpeedReduced = false;
    }

    public override void ExitState()
    {
        print("Exiting jumping state...");
        GetCTX<PlayerStateMachine>().JumpBuffer = _jumpBuffer;
    }

    protected override void CollisionEnter(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo")) _isGrounded = true;
    }

    protected override void UpdateState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _jumpBuffer = GetCTX<PlayerStateMachine>().JumpBufferTime;
        }
        else if(_jumpBuffer > 0)
        {
            _jumpBuffer -= Time.deltaTime;
        }

        if (!_airSpeedReduced && !Input.GetKey(KeyCode.Space) && _rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.3f);
            _airSpeedReduced = true;
        }
    }
}
