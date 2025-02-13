using UnityEngine;
public class PlayerIdleState : BaseState
{
    [SerializeField] Rigidbody2D rb;

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
        if (Input.GetAxisRaw("Horizontal") != 0) ChangeState(Ctx.GetStateByType<PlayerMoveState>());
    }
}
