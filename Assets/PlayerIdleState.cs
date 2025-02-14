using UnityEngine;
public class PlayerIdleState : BaseState
{

    public override void CheckSwitchState()
    {
        if (Input.GetAxisRaw("Horizontal") != 0) ChangeState(Ctx.GetStateByType<PlayerMoveState>());
    }

    public override void EnterState()
    {
        print("Entering IdleState...");
    }

    public override void ExitState()
    {
        print("Exiting IdleState...");
    }


    protected override void UpdateState()
    {
        
    }
}
