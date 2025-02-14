using UnityEngine;

public class IsGroundedState : BaseState
{
    public override void CheckSwitchState()
    {
        if (Input.GetKeyDown(KeyCode.Space) || GetCTX<PlayerStateMachine>().JumpBuffer > 0) ChangeState(Ctx.GetStateByType<PlayerJumpingState>());
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
