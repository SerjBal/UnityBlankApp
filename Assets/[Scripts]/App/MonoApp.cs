using UnityEngine;
using System.Collections.Generic;

namespace Serjbal.App
{
    public class MonoApp : MonoBehaviour
    {
        [SerializeField] protected AppSettings _settings;
        [SerializeField] protected List<MonoBehaviour> _staticServices;
        protected App _app;
        protected AppInjector _appInjector = new AppInjector();


        private void Awake()
        {
            BuildApp();
            AddServices();
            AddStates();
            BindHendlers();
            ExecuteInitStates();

            _app.StateMachine.SwitchToState<AppInitedState>();
            _app.EventBus.Raise(new AppInitedEvent(this));
        }

        private void OnDestroy()
        {
            _app.EventBus.Raise(new AppBeforeDisposeEvent(this));

            ExecuteDisposeStates();

            _app.EventBus.Raise(new AppDisposedEvent(this));

            UnbindHendlers();
        }

        protected virtual void BuildApp()
        {
            _app = new AppBuilder().SetAppSettings(_settings).Build();
        }

        protected virtual void AddServices()
        {
            var jsonSettingsStorage = new JsonLocalStorage<AppSettingsModel>(_app.Settings.config);
            _app.ServiceContainer.AddService(new SettingsStorage(jsonSettingsStorage));
            _app.ServiceContainer.AddService(new SceneSys());

            foreach (var service in _staticServices)
            {
                _app.ServiceContainer.AddService(service.GetType(), service);
            }
        }

        protected virtual void AddStates()
        {
            //inject
            _app.StateMachine.AddState(new InjectAppToServicesState(_app, _appInjector));
            _app.StateMachine.AddState(new InjectServicesToServicesState(_app, _appInjector));
            //init
            _app.StateMachine.AddState(new InitAppServicesState(_app));
            //dispose
            _app.StateMachine.AddState(new DisposeAppServcesState(_app));
            //start & end
            _app.StateMachine.AddState(new AppInitedState(_app));
            _app.StateMachine.AddState(new AppDisposedState(_app));
        }

        protected virtual void BindHendlers()
        {
            ((INotifyStateChanged)_app.StateMachine).OnEnterState += OnEnterState;
        }

        protected virtual void UnbindHendlers()
        {
            ((INotifyStateChanged)_app.StateMachine).OnEnterState -= OnEnterState;
        }

        protected virtual void ExecuteInitStates()
        {
            //inject
            _app.StateMachine.SwitchToState<InjectAppToServicesState>().Execute();
            _app.StateMachine.SwitchToState<InjectServicesToServicesState>().Execute();
            //init
            _app.StateMachine.SwitchToState<InitAppServicesState>().Execute();
        }

        protected virtual void ExecuteDisposeStates()
        {
            _app.StateMachine.SwitchToState<DisposeAppServcesState>().Execute();
        }

        protected virtual void OnEnterState(IState state)
        {
            _app.EventBus.Raise(new AppChangeStateEvent(this, state));
        }
    }
}

