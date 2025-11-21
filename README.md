# UnityBlankApp 🏗️

Архитектурный шаблон для Unity, реализующий чистую архитектуру, паттерн MVVM и современные практики разработки.
Этот шаблон предназначен для расширения и настройки под конкретные нужды проектов.

## ✨ Особенности

### Чистая архитектура
- Слоистая архитектура с четкими границами и направлением зависимостей
- Разделение ответственности между системами
- Тестируемая модульная структура

### Управление состояниями
- Конечный автомат состояний для управления жизненным циклом приложения (`StateMachine`)
- Расширяемая система состояний с инициализацией, внедрением зависимостей, выполнением и очисткой

### Внедрение зависимостей
- Кастомный DI-контейнер (`ServiceContainer`)
- AppInjector для автоматического разрешения зависимостей
- Управление жизненным циклом сервисов

### Система событий
- Типобезопасная шина событий (`IEventBus<T>`)
- Слабая связанность между компонентами
- Событийно-ориентированная архитектура для коммуникации систем

### Защита от взлома
- Зашифрованные типы данных (`SfaeInt, SafeFloat`)
- Восстановление измененных данных

## 🚀 Быстрый старт

### 1. Базовая настройка

Создайте точку входа вашего приложения, наследуясь от `MonoApp`:

```csharp
public class MyGameApp : MonoApp
{
    [SerializeField] private AppSettings _gameSettings;
    [SerializeField] private List<MonoBehaviour> _gameServices;

    protected override void BuildApp()
    {
        _app = new AppBuilder()
            .SetAppSettings(_gameSettings)
            .Build();
    }

    protected override void AddServices()
    {
        // Регистрируйте ваши кастомные сервисы
        _app.ServiceContainer.AddService(new GameManager());
        _app.ServiceContainer.AddService(new AchievementSystem());
    }
}
```

### 2. Создание сервисов

Реализуйте сервисы с внедрением зависимостей:

```csharp
public class GameManager : IService, IInitializable
{
    [InjectDependency] private App _app;
    [InjectDependency] private IEventBus<AppEvent> _eventBus;

    public void Init()
    {
        _eventBus.Subscribe<AppInitedEvent>(OnAppInitialized);
    }

    private void OnAppInitialized(AppInitedEvent appEvent)
    {
        // Запускайте вашу игровую логику
    }
}
```


### 3. Пользовательские состояния

Расширьте машину состояний для нужд вашего приложения:

```csharp
public class GameplayState : IState
{
    private readonly App _app;

    public GameplayState(App app)
    {
        _app = app;
    }

    public void Execute()
    {
        // Логика инициализации геймплея
        _app.EventBus.Raise(new GameplayStartedEvent());
    }
}
```

## 🛠️ Основные компоненты

### AppBuilder

fluent-подобный строитель приложения:

```csharp
var app = new AppBuilder()
    .SetAppSettings(settings)
    .SetStateMachine(customStateMachine)
    .SetServiceContainer(customContainer)
    .Build();
```

### ServiceContainer

Легковесный DI-контейнер:

```csharp
// Регистрация
serviceContainer.AddService<ISceneService>(new SceneSys());

// Получение
var sceneService = serviceContainer.GetService<ISceneService>();
```

### StateMachine

```csharp
_stateMachine.AddState(new InjectAppToServicesState(_app, _appInjector));
_stateMachine.SwitchToState<InitAppServicesState>().Execute();
```
