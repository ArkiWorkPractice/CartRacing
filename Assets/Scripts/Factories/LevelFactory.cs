using Levels;
using UnityEngine;

namespace Factories
{
    public class LevelFactory
    {
        private Level _currentLevel;

        public Level CurrentLevel => _currentLevel;

        public Level Create(Level levelPrefab)
        {
            Remove();

            _currentLevel = Object.Instantiate(levelPrefab);
            _currentLevel.transform.position = Vector3.zero;

            return _currentLevel;
        }

        public void Remove()
        {
            if (!_currentLevel)
                return;

            Object.Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }
    }
}