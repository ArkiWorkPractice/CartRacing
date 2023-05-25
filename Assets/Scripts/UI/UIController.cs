using System;
using UnityEngine;
using UnityEngine.UIElements;
using ServiceLocatorModule;
using ServiceLocatorModule.Interfaces;

namespace UI
{
    public class UIController : MonoBehaviour, IService
    {
        public event Action<int> OnSelectLevel;
        public event Action OnLoadMenu;
        public event Action OnRestartLevel;
        public event Action OnResumeLevel;
        public event Action OnPause;
        public event Action OnEndGame;

        [Header("Game menus")]
        [SerializeField] private VisualTreeAsset pauseAsset;
        [SerializeField] private VisualTreeAsset endGameAsset;
        [SerializeField] private VisualTreeAsset mainMenuAsset;
        [SerializeField] private VisualTreeAsset playerHudAsset;
        [Header("Main menu components")]
        [SerializeField] private VisualTreeAsset mainMenuItemsAsset;
        [SerializeField] private VisualTreeAsset selectLevelAsset;

        private UIDocument _uiDocument;
        private VisualElement _menuWrapperVisualEl;
        private VisualElement _mainMenuVisualEl;
        private VisualElement _selectLevelVisualEl;
        private VisualElement _playerHudVisualEl;
        private VisualElement _pauseVisualEl;
        private VisualElement _endGameVisualEl;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _menuWrapperVisualEl = _uiDocument.rootVisualElement.Q<VisualElement>("MainMenuItemsWrapper");

            SignMainMenu();
            SignPlayerHud();
            SignPauseMenu();
            SignEndGameMenu();

            LoadMainMenu();

            ServiceLocator.Instance.RegisterService<UIController>(this);
        }

        public void MoveToMainMenu()
        {
            LoadMainMenu();
            OnLoadMenu?.Invoke();
        }

        public void RestartLevel()
        {
            LoadPlayerHud();
            OnRestartLevel?.Invoke();
        }

        public void ResumeLevel()
        {
            LoadPlayerHud();
            OnResumeLevel?.Invoke();
        }

        public void LoadPlayerHud()
        {
            _uiDocument.visualTreeAsset = playerHudAsset;
        }

        public void LoadPauseMenu()
        {
            _uiDocument.visualTreeAsset = pauseAsset;
            OnPause?.Invoke();
        }

        public void LoadEndGameMenu()
        {
            _uiDocument.visualTreeAsset = endGameAsset;
            OnEndGame?.Invoke();
        }

        public void ChangeHealthValue(int newHealthValue)
        {
            _playerHudVisualEl.Q<Label>($"HealthValue").text = $"Health: {newHealthValue}";
        }

        private void SignMainMenu()
        {
            _mainMenuVisualEl = mainMenuItemsAsset.CloneTree();
            _selectLevelVisualEl = selectLevelAsset.CloneTree();

            _mainMenuVisualEl.Q<Button>("PlayButton").clicked += LoadSelectLevelMenu;
            _mainMenuVisualEl.Q<Button>("ExitButton").clicked += Exit;

            _selectLevelVisualEl.Q<Button>("Level1Button").clicked += () => OnSelectLevel?.Invoke(1);
            _selectLevelVisualEl.Q<Button>("Level2Button").clicked += () => OnSelectLevel?.Invoke(2);
            _selectLevelVisualEl.Q<Button>("Level3Button").clicked += () => OnSelectLevel?.Invoke(3);
            _selectLevelVisualEl.Q<Button>("BackToMenuButton").clicked += LoadMainMenu;
        }

        private void SignPlayerHud()
        {
            _playerHudVisualEl = playerHudAsset.CloneTree();
        }

        private void SignPauseMenu()
        {
            _pauseVisualEl = pauseAsset.CloneTree();

            _pauseVisualEl.Q<Button>("ResumeButton").clicked += ResumeLevel;
            _pauseVisualEl.Q<Button>("RestartButton").clicked += RestartLevel;
            _pauseVisualEl.Q<Button>("ExitButton").clicked += MoveToMainMenu;
        }

        private void SignEndGameMenu()
        {
            _endGameVisualEl = endGameAsset.CloneTree();

            _endGameVisualEl.Q<Button>("RestartButton").clicked += RestartLevel;
            _endGameVisualEl.Q<Button>("ExitButton").clicked += MoveToMainMenu;
        }

        private void LoadMainMenu()
        {
            _menuWrapperVisualEl.Clear();
            _menuWrapperVisualEl.Add(_mainMenuVisualEl);
        }

        private void LoadSelectLevelMenu()
        {
            _menuWrapperVisualEl.Clear();
            _menuWrapperVisualEl.Add(_selectLevelVisualEl);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}