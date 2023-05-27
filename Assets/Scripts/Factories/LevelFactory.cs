using Levels;
using ServiceLocatorModule.Interfaces;
using UnityEngine;

namespace Factories
{
    public class LevelFactory :IService
    {
        private Level _currentLevel;

        public Level CurrentLevel => _currentLevel;

        public Level Create(Level levelPrefab)
        {
            if (_currentLevel!= null)
            {
                Remove();
            }
            _currentLevel = Object.Instantiate(levelPrefab);
            _currentLevel.transform.position = Vector3.zero;

            return _currentLevel;
        }

        public void Remove()
        {
            Object.Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }
    }
}
