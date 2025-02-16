using System;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    #region State Factory
    [SerializeField] BaseState[] _states;

    private void Awake()
    {
        foreach (BaseState state in _states)
        {
            state.SetupState(this);
        }

        OnAwake();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }

    /// <param name="index">Index of the desired state in the states array set in Unity's editor</param>
    /// <returns>Returns the state with index of <paramref name="index"/> as set in the editor.</returns>
    /// <exception cref="UnityException">If the index is outside bounds</exception>
    public BaseState GetStateByIndex(ushort index)
    {
        if (index >= 0 && index < _states.Length) return _states[index];
        else throw new UnityException($"State with index {index} does not exist in {this.name}");
    }

    /// <typeparam name="T">Type of the state wanted.</typeparam>
    /// <returns>Returns the first state of type <typeparamref name="T"/> in the states array.</returns>
    public T GetStateByType<T>() where T : BaseState
    {
        foreach (BaseState state in _states) if (state.GetType() == typeof(T)) return (T)state;
        return null;
    }

    /// <param name="type">Type of the state wanted.</param>
    /// <returns>Returns the first state of type <typeparamref name="T"/> in the states array.</returns>
    public BaseState GetStateByType(Type type)
    {
        foreach (BaseState state in _states) if (state.GetType() == type) return state;
        return null;
    }
    #endregion

    /// <summary>
    /// State machine's current state.
    /// </summary>
    public BaseState CurrState { get; set; }

    private void Start()
    {
        CurrState = GetStateByIndex(0);
        CurrState.EnterState();

        OnStart();
    }

    private void Update()
    {
        CurrState.UpdateStates();

        OnUpdate();
    }
    private void FixedUpdate()
    {
        CurrState.FixedUpdateStates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrState.ExecuteTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrState.ExecuteTriggerExit(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrState.ExecuteTriggerStay(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrState.ExecuteCollisionEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CurrState.ExecuteCollisionExit(collision);
    }
}
