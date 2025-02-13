using System;
using UnityEngine;

public sealed class StateFactory : MonoBehaviour
{
    [SerializeField] StateMachine _ctx;
    [SerializeField] BaseState[] _states;

    private void Awake()
    {
        foreach (BaseState state in _states)
        {
            //state.SetupState(_ctx, this);
        }
    }

    public BaseState GetStateByIndex(ushort index) 
    { 
        if(index >= 0 && index < _states.Length) return _states[index]; 
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
}
