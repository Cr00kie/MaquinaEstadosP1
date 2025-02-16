using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeReference] bool _isRootState = false;

    BaseState _currParentState;
    BaseState _currSubState;

    /// <summary>
    /// The state's current context.
    /// </summary>
    protected StateMachine Ctx { get; private set; }

    /// <summary>
    /// Method to access the specific context of a state.
    /// </summary>
    /// <typeparam name="StateMachineType"></typeparam>
    /// <returns>Returns the state's context downcasted as <typeparamref name="StateMachineType"/> or null if the downcasting wasn't possible</returns>
    protected StateMachineType GetCTX<StateMachineType>() where StateMachineType : StateMachine
    {
        return Ctx as StateMachineType;
    }

    public void SetupState(StateMachine ctx)
    {
        Ctx = ctx;
        OnStateSetUp();
    }

    /// <summary>
    /// Method called after the context for the state is set.
    /// </summary>
    public virtual void OnStateSetUp() { }

    /// <summary>
    /// Method called when transitioning into the state.
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// Method called every frame when the current state is this state.
    /// </summary>
    protected abstract void UpdateState();

    /// <summary>
    /// Method called before transitioning into another state.
    /// </summary>
    public abstract void ExitState();

    /// <summary>
    /// Method called after UpdateState to check for state transitions.
    /// </summary>
    public abstract void CheckSwitchState();
    public void UpdateStates()
    {
        UpdateState();
        CheckSwitchState();
        if (_currSubState != null) _currSubState.UpdateStates();
    }
    protected virtual void FixedUpdateState() { }
    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if(_currSubState != null) _currSubState.FixedUpdateState();
    }

    /// <summary>
    /// Changes the context current state to <paramref name="newState"/> if the current state is a rootState.
    /// Otherwise changes the current state's parent to <paramref name="newState"/>.
    /// </summary>
    /// <param name="newState">The state to transition to.</param>
    /// <exception cref="UnityException">Called if the transition wasn't possible.</exception>
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

    /// <summary>
    /// Method used to change the substate.
    /// </summary>
    /// <param name="state">The new substate to transition to.</param>
    public void SetSubState(BaseState state)
    {
        _currSubState = state;
        _currSubState.SetParentState(this);
    }

    /// <summary>
    /// Method used to change the parentstate.
    /// </summary>
    /// <param name="state">The new parent state for the state-</param>
    public void SetParentState(BaseState state)
    {
        _currParentState = state;
    }
}
