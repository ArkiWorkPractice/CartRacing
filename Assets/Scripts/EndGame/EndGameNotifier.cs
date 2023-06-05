using System;
using EventBusModule;
using ServiceLocatorModule;
using Services;
using UnityEngine;

namespace EndGame
{
    public class EndGameNotifier : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            ServiceLocator.Instance.GetService<EventBus>().Raise(EventBusDefinitions.EndGameActionKey, new EventBusArgs());
        }
    }
}
