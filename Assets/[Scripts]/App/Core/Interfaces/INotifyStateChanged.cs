using System;

namespace Serjbal.App
{
     public interface INotifyStateChanged
    {
        event Action<IState> OnEnterState;
        event Action<IState> OnExitState;
        event Action<IState, IState> OnStateChanged;
    }
}