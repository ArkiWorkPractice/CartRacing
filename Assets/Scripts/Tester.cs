using System;
using System.Collections;
using System.Collections.Generic;
using Factories;
using Levels;
using ObstacleSpawn;
using ServiceLocatorModule;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    [SerializeField] private Button button;
    private LevelLoader _levelLoader;
    [SerializeField] private ConfigsProvider configsProvider;
    [SerializeField] private PrefabProvider prefabProvider;
    private ObstacleSpawner _obstacleSpawner;
    [SerializeField] private int levelNumber;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(configsProvider);
        ServiceLocator.Instance.RegisterService(prefabProvider);
    }

    public void Start()
    {
        _levelLoader = new LevelLoader();
        button.onClick.AddListener(() => _levelLoader.EnterLoadLevelState(levelNumber));
    }
    
    public void EnterBootstrapState()
    {
        ServiceLocator.Instance.RegisterService(new LevelLoader());
    }
}
