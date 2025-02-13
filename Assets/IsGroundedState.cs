using UnityEngine;

public class IsGroundedState : BaseState
{
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
        if(Input.GetKeyDown(KeyCode.Space)) ChangeState(Ctx.GetStateByType<PlayerJumpingState>());
    }
}
