using UnityEngine;

public class PlayerMoveState : BaseState
{
    [SerializeField] float speed;
    float mov;
    PlayerStateMachine ctx;

    public override void OnStateSetUp()
    {
        ctx = GetCTX<PlayerStateMachine>();
    }
    public override void EnterState()
    {
        print("Entering moving state...");
    }

    public override void ExitState()
    {
        print("Exiting moving state...");
    }

    public override void CheckSwitchState()
    {
        if(mov == 0) ChangeState(Ctx.GetStateByType<PlayerIdleState>());
    }

    protected override void UpdateState()
    {
        mov = ctx.playerInputActions.Player.Movement.ReadValue<float>();

        transform.position = new Vector2(transform.position.x + mov * speed * Time.deltaTime, transform.position.y);
    }
}
