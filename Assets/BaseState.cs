using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeReference] bool _isRootState = false;

    BaseState _currParentState;
    BaseState _currSubState;

    protected StateMachine Ctx { get; private set; }
    //protected StateFactory StateFactory { get; private set; }


    public void SetupState(StateMachine ctx)
    {
        Ctx = ctx;
    }

    public abstract void EnterState();
    protected abstract void UpdateState();
    public abstract void ExitState();
    public void UpdateStates()
    {
        UpdateState();
        if (_currSubState != null) _currSubState.UpdateStates();
    }
    public void ChangeState(BaseState newState)
    {
        ExitState();
        newState.EnterState();
        if (_isRootState) Ctx.CurrState = newState;
        else if (_currParentState != null) _currParentState.SetSubState(newState);
        else throw new UnityException($"Current state, {Ctx.CurrState}, is a substate without a parent. Can't change to {newState.GetType()} state");
    }

    protected virtual void TriggerEnter(Collider2D other) { }
    protected virtual void TriggerExit(Collider2D other) { }
    protected virtual void TriggerStay(Collider2D other) { }
    protected virtual void CollisionEnter(Collision2D other) { }
    protected virtual void CollisionExit(Collision2D other) { }

    public void ExecuteTriggerEnter(Collider2D other)
    {
        TriggerEnter(other);
        if(_currSubState != null) _currSubState.ExecuteTriggerEnter(other);
    }
    public void ExecuteTriggerExit(Collider2D other)
    {
        TriggerExit(other);
        if (_currSubState != null) _currSubState.ExecuteTriggerExit(other);
    }
    public void ExecuteTriggerStay(Collider2D other)
    {
        TriggerStay(other);
        if (_currSubState != null) _currSubState.ExecuteTriggerStay(other);
    }
    public void ExecuteCollisionEnter(Collision2D collision)
    {
        CollisionEnter(collision);
        if (_currSubState != null) _currSubState.ExecuteCollisionEnter(collision);
    }
    public void ExecuteCollisionExit(Collision2D collision)
    {
        CollisionExit(collision);
        if (_currSubState != null) _currSubState.ExecuteCollisionExit(collision);
    }
    public void SetSubState(BaseState state)
    {
        _currSubState = state;
        _currSubState.SetParentState(this);
    }
    public void SetParentState(BaseState state)
    {
        _currParentState = state;
    }
}
