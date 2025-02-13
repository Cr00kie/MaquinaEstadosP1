using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //[SerializeReference] StateFactory _stateFactory;

    #region State Factory
    [SerializeField] BaseState[] _states;

    private void Awake()
    {
        foreach (BaseState state in _states)
        {
            state.SetupState(this);
        }
    }

    public BaseState GetStateByIndex(ushort index)
    {
        if (index >= 0 && index < _states.Length) return _states[index];
        else throw new UnityException($"State with index {index} does not exist in {this.name}");
    }

    public T GetStateByType<T>() where T : BaseState
    {
        foreach (BaseState state in _states) if (state.GetType() == typeof(T)) return (T)state;
        return null;
    }
    public BaseState GetStateByType(Type type)
    {
        foreach (BaseState state in _states) if (state.GetType() == type) return state;
        return null;
    }
    #endregion

    public BaseState CurrState { get; set; }

    private void Start()
    {
        //CurrState = _stateFactory.GetStateByIndex(0);
        CurrState = GetStateByIndex(0);
        CurrState.EnterState();
    }

    private void Update()
    {
        CurrState.UpdateStates();
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
