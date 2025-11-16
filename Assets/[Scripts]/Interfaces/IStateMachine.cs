using System;

namespace Serjbal.App
{
    public interface IStateMachine<TState>
    {
        void AddState(TState state);
        void RemoveState(TState state);
        TState GetCurrentState();
        T SwitchToState<T>() where T : TState;
    }
}