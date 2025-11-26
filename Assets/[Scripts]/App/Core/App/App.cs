
namespace Serjbal.App
{
    public class App
    {
        public readonly AppSettings Settings;
        public readonly IStateMachine<AppState> StateMachine;
        public readonly IServiceContainer ServiceContainer;
        public readonly IEventBus<AppEvent> EventBus;

        public App(AppSettings settings,
            IStateMachine<AppState> stateMashine,
            IServiceContainer serviceContainer,
            IEventBus<AppEvent> eventBus)
        {
            Settings = settings;
            StateMachine = stateMashine;
            ServiceContainer = serviceContainer;
            EventBus = eventBus;
        }
    }
}
