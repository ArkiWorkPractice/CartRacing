using UnityEngine;
using UnityEngine.UIElements;
using ServiceLocatorModule;
using EventBusModule;
using EventBusModule.Interfaces;
using ServiceLocatorModule.Interfaces;

namespace UI
{
    public class UIController : MonoBehaviour, IService
    {
        [Header("Game menus")]
        [SerializeField] private VisualTreeAsset mainMenuAsset;
        [SerializeField] private VisualTreeAsset selectLevelMenuAsset;
        [SerializeField] private VisualTreeAsset pauseAsset;
        [SerializeField] private VisualTreeAsset endGameAsset;
        [SerializeField] private VisualTreeAsset playerHudAsset;

        private UIDocument _uiDocument;
        private EventBus _eventBus;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void Initialize()
        {
            _uiDocument = GetComponent<UIDocument>();
            _eventBus = ServiceLocator.Instance.GetService<EventBus>();
            _eventBus.Subscribe<EventBusEventArgs>("OnStartGame", LoadMainMenu);

            _eventBus.Subscribe<EventBusEventArgs>("OnLoadSelectLevel", LoadSelectLevelMenu);
            _eventBus.Subscribe<EventBusEventArgs>("OnExit", Exit);
            _eventBus.Subscribe<EventBusEventArgs>("OnLoadMainMenu", LoadMainMenu);
            _eventBus.Subscribe<EventBusEventArgs>("OnPauseGame", LoadPauseMenu);
            _eventBus.Subscribe<EventBusEventArgs>("OnEndGame", LoadEndGameMenu);
            _eventBus.Subscribe<EventBusEventArgs>("OnStartRace", LoadPlayerHud);
            _eventBus.Subscribe<EventBusEventArgs>("OnResumeGame", LoadPlayerHud);
            _eventBus.Subscribe<EventBusEventArgs>("OnRestartGame", LoadPlayerHud);
            _eventBus.Subscribe<SingleIntParameterEventBusEventArgs>("OnUpdateHealthValue", UpdateHealthValue);
        }

        private void LoadMainMenu(IEventBusEventArgs e)
        {
            _uiDocument.visualTreeAsset = mainMenuAsset;
            var visualElement = _uiDocument.rootVisualElement.Q<VisualElement>("MainMenuItemsWrapper");

            visualElement.Q<Button>("PlayButton").clicked += () => _eventBus.Raise("OnLoadSelectLevel", new EventBusEventArgs());
            visualElement.Q<Button>("ExitButton").clicked += () => _eventBus.Raise("OnExit", new EventBusEventArgs());
        }

        private void LoadSelectLevelMenu(IEventBusEventArgs e)
        {
            _uiDocument.visualTreeAsset = selectLevelMenuAsset;
            var visualElement = _uiDocument.rootVisualElement.Q<VisualElement>("SelectLevelItemsWrapper");

            visualElement.Q<Button>("BackToMenuButton").clicked += () => _eventBus.Raise("OnLoadMainMenu", new EventBusEventArgs());
            //itemsAsset.Q<Button>("Level1Button").clicked += () => OnSelectLevel?.Invoke(1);
            //itemsAsset.Q<Button>("Level2Button").clicked += () => OnSelectLevel?.Invoke(2);
            //itemsAsset.Q<Button>("Level3Button").clicked += () => OnSelectLevel?.Invoke(3);
        }

        private void LoadPlayerHud(IEventBusEventArgs e)
        {
            _uiDocument.visualTreeAsset = playerHudAsset;
        }

        private void UpdateHealthValue(IEventBusEventArgs e)
        {
            _uiDocument.rootVisualElement.Q<Label>("HealthValue").text = $"Health: {((SingleIntParameterEventBusEventArgs)e).Number}";
        }

        private void LoadPauseMenu(IEventBusEventArgs e)
        {
            _uiDocument.visualTreeAsset = pauseAsset;
            var visualElement = _uiDocument.rootVisualElement.Q<VisualElement>("PauseMenuWRapper");

            visualElement.Q<Button>("ResumeButton").clicked += () => _eventBus.Raise("OnResumeGame", new EventBusEventArgs());
            visualElement.Q<Button>("RestartButton").clicked += () => _eventBus.Raise("OnRestartGame", new EventBusEventArgs());
            visualElement.Q<Button>("ExitButton").clicked += () => _eventBus.Raise("OnLoadMainMenu", new EventBusEventArgs());
        }

        private void LoadEndGameMenu(IEventBusEventArgs e)
        {
            _uiDocument.visualTreeAsset = pauseAsset;
            var visualElement = _uiDocument.rootVisualElement.Q<VisualElement>("EndGameMenuWrapper");

            visualElement.Q<Button>("RestartButton").clicked += () => _eventBus.Raise("OnRestartGame", new EventBusEventArgs());
            visualElement.Q<Button>("ExitButton").clicked += () => _eventBus.Raise("OnLoadMainMenu", new EventBusEventArgs());
        }

        private void Exit(IEventBusEventArgs e)
        {
            Application.Quit();
        }
    }
}