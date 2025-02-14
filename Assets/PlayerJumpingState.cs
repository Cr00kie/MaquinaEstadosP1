using UnityEngine;

public class PlayerJumpingState : BaseState
{
    [SerializeField] float jumpStrength;
    [SerializeField] Rigidbody2D rb;
    bool isGrounded;

    public override void CheckSwitchState()
    {
        if (rb.velocity.y < 0f && isGrounded) ChangeState(Ctx.GetStateByType<IsGroundedState>());
    }

    public override void EnterState()
    {
        SetSubState(Ctx.GetStateByType<PlayerIdleState>());
        print("Entering jumping state...");
        isGrounded = false;
        rb.velocity += new Vector2(0,jumpStrength);
    }

    public override void ExitState()
    {
        print("Exiting jumping state...");
    }

    protected override void CollisionEnter(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo")) isGrounded = true;
    }

    protected override void UpdateState()
    {
        
    }
}
