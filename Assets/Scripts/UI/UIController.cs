using UnityEngine;
using UnityEngine.UIElements;
using ServiceLocatorModule;
using EventBusModule;
using EventBusModule.Interfaces;
using ServiceLocatorModule.Interfaces;
using Unity.VisualScripting;
using EventBus = EventBusModule.EventBus;

namespace UI
{
    public class UIController : MonoBehaviour, IService
    {
        [Header("Menu pages")] [SerializeField]
        private VisualTreeAsset mainMenuAsset;

        [SerializeField] private VisualTreeAsset selectLevelMenuAsset;
        [SerializeField] private VisualTreeAsset pauseAsset;
        [SerializeField] private VisualTreeAsset endGameAsset;
        [SerializeField] private VisualTreeAsset playerHudAsset;

        [Header("Main document")] [SerializeField]
        private UIDocument uiDocument;

        private EventBus _eventBus;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }

        public void Initialize()
        {
            _eventBus = ServiceLocator.Instance.GetService<EventBus>();

            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.StartGameActionKey, LoadMainMenu);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.LoadSelectLevelMenuActionKey, LoadSelectLevelMenu);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.ExitFromGameActionKey, Exit);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.LoadMainMenuActionKey, LoadMainMenu);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.PauseGameActionKey, LoadPauseMenu);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.EndGameActionKey, LoadEndGameMenu);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.StartRaceActionKey, LoadPlayerHud);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.ResumeGameActionKey, LoadPlayerHud);
            _eventBus.Subscribe<EventBusArgs>(EventBusDefinitions.RestartGameActionKey, LoadPlayerHud);
            _eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.UpdateHealthValueActionKey,
                UpdateHealthValue);
            //_eventBus.Subscribe<SingleIntParameterEventBusArgs>(EventBusDefinitions.LoadLevelActionKey, UpdateHealthValue);
        }

        private void LoadMainMenu(IEventBusArgs e)
        {
            uiDocument.visualTreeAsset = mainMenuAsset;
            var visualElement = uiDocument.rootVisualElement.Q<VisualElement>("MainMenuItemsWrapper");

            visualElement.Q<Button>("PlayButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadSelectLevelMenuActionKey, new EventBusArgs());
            visualElement.Q<Button>("ExitButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.ExitFromGameActionKey, new EventBusArgs());
        }

        private void LoadSelectLevelMenu(IEventBusArgs e)
        {
            uiDocument.visualTreeAsset = selectLevelMenuAsset;
            var visualElement = uiDocument.rootVisualElement.Q<VisualElement>("SelectLevelItemsWrapper");

            visualElement.Q<Button>("BackToMenuButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
            visualElement.Q<LevelButton>("Level0Button").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadLevelActionKey,
                    new SingleIntParameterEventBusArgs(visualElement.Q<LevelButton>("Level0Button").LevelNumber));
            visualElement.Q<LevelButton>("Level1Button").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadLevelActionKey,
                    new SingleIntParameterEventBusArgs(visualElement.Q<LevelButton>("Level1Button").LevelNumber));
            visualElement.Q<LevelButton>("Level2Button").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadLevelActionKey,
                    new SingleIntParameterEventBusArgs(visualElement.Q<LevelButton>("Level2Button").LevelNumber));
        }

        private void LoadPlayerHud(IEventBusArgs e)
        {
            uiDocument.visualTreeAsset = playerHudAsset;
        }

        private void UpdateHealthValue(IEventBusArgs e)
        {
            uiDocument.rootVisualElement.Q<Label>("HealthValue").text =
                $"Health: {((SingleIntParameterEventBusArgs)e).Number}";
        }

        private void LoadPauseMenu(IEventBusArgs e)
        {
            uiDocument.visualTreeAsset = pauseAsset;
            var visualElement = uiDocument.rootVisualElement.Q<VisualElement>("PauseMenuWrapper");

            visualElement.Q<Button>("ResumeButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.ResumeGameActionKey, new EventBusArgs());
            visualElement.Q<Button>("RestartButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.RestartGameActionKey, new EventBusArgs());
            visualElement.Q<Button>("ExitButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
        }

        private void LoadEndGameMenu(IEventBusArgs e)
        {
            uiDocument.visualTreeAsset = endGameAsset;
            var visualElement = uiDocument.rootVisualElement.Q<VisualElement>("EndGameMenuWrapper");

            visualElement.Q<Button>("RestartButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.RestartGameActionKey, new EventBusArgs());
            visualElement.Q<Button>("ExitButton").clicked += () =>
                _eventBus.Raise(EventBusDefinitions.LoadMainMenuActionKey, new EventBusArgs());
        }

        private void Exit(IEventBusArgs e)
        {
            Application.Quit();
        }
    }
}