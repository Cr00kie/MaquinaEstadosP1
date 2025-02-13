using UnityEngine;

public class PlayerMoveState : BaseState
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    public override void EnterState()
    {
        print("Entering moving state...");
    }

    public override void ExitState()
    {
        print("Exiting moving state...");
    }

    protected override void UpdateState()
    {
        float mov = Input.GetAxisRaw("Horizontal");

        transform.position = new Vector2(transform.position.x + mov * speed * Time.deltaTime, transform.position.y);

        if (mov == 0) ChangeState(Ctx.GetStateByType<PlayerIdleState>());
    }
}
