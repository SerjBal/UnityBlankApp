using System;

namespace Serjbal.App
{
    public class AppBuilder
    {
        protected AppSettings _settings;
        protected IStateMachine<AppState> _stateMachine;
        protected IServiceContainer _serviceContainer;
        protected IEventBus<AppEvent> _eventBus;

        public virtual App Build()
        {
            SetDefaults();

            return new App(
                _settings,
                _stateMachine,
                _serviceContainer,
                _eventBus
                );
        }

        protected void SetDefaults()
        {
            _settings ??= new AppSettings();
            _stateMachine ??= new StateMachine();
            _serviceContainer ??= new ServiceContainer();
            _eventBus ??= new AppEventBus<AppEvent>();
        }

        public AppBuilder SetAppSettings(AppSettings appSettings)
        {
            if (_settings != null)
                throw new Exception("Settings is already set");

            _settings = appSettings;

            return this;
        }

        public AppBuilder SetStateMachine(IStateMachine<AppState> stateMachine)
        {
            if (_stateMachine != null)
                throw new Exception("StateMachine is already set");

            _stateMachine = stateMachine;

            return this;
        }

        public AppBuilder SetServiceContainer(IServiceContainer serviceContainer)
        {
            if (_serviceContainer != null)
                throw new Exception("ServiceContainer is already set");

            _serviceContainer = serviceContainer;

            return this;
        }

        public AppBuilder SetStateMachine(IEventBus<AppEvent> eventBus)
        {
            if (_eventBus != null)
                throw new Exception("EventBus is already set");

            _eventBus = eventBus;

            return this;
        }
    }
}