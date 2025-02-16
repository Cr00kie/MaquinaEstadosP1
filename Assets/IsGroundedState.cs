using UnityEngine;

public class IsGroundedState : BaseState
{
    PlayerStateMachine ctx;
    public override void OnStateSetUp()
    {
        ctx = GetCTX<PlayerStateMachine>();
    }
    public override void CheckSwitchState()
    {
        if (ctx.IsJumpPressed || ctx.JumpBuffer > 0) ChangeState(Ctx.GetStateByType<PlayerJumpingState>());
    }

    public override void EnterState()
    {
        print("Entering isGroundedState ...");
        SetSubState(Ctx.GetStateByType<PlayerIdleState>());
    }

    public override void ExitState()
    {
        print("Exiting isGroundedState ...");
    }

    protected override void UpdateState()
    {
        
    }
}
