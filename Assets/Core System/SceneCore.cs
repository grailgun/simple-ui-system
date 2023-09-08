using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-2)]
    public class SceneCore : MonoBehaviour
    {
        public Dictionary<EnumId, SceneService> Services => _cachedServices;

        [Header("Scene Services")]
        [SerializeField]
        private List<SceneService> services = new List<SceneService>();

        private Dictionary<EnumId, SceneService> _cachedServices = new Dictionary<EnumId, SceneService>();
        
        private void Awake()
        {
            var servicesOnScene = GameObject.FindGameObjectsWithTag(SceneServiceProvider.SCENE_SERVICE_TAG);

            foreach (var serviceGO in servicesOnScene)
            {
                var service = serviceGO.GetComponent<SceneService>();
                if (services.Contains(service) == false)
                    services.Add(service);
            }

            foreach (var service in services)
            {
                if(service.ServiceId != null)
                    _cachedServices.TryAdd(service.ServiceId, service);

                service.InjectCore(this);
            }
        }

        private IEnumerator Start()
        {
            foreach (var service in services)
            {
                yield return service.StartService();
                service.Init();
            }
        }
    }

    public static class SceneServiceProvider
    {
        public const string SCENE_CORE_TAG = "GameController";        
        public const string SCENE_SERVICE_TAG = "SceneService";

        private static SceneCore _cachedCore;

        public static SceneCore GetSceneCore()
        {
            if (_cachedCore != null)
                return _cachedCore;

            var gameObject = GameObject.FindGameObjectWithTag(SCENE_CORE_TAG);
            _cachedCore = gameObject.GetComponent<SceneCore>();

            if (_cachedCore == null)
                Debug.LogWarning("No scene core in this scene");

            return _cachedCore;
        }

        public static T GetService<T>(EnumId serviceId) where T : SceneService
        {
            var core = GetSceneCore();

            if (core.Services.TryGetValue(serviceId, out var service) == false)
            {
                Debug.LogWarning($"No services found in the scene");
            }

            return (T)service;
        }

        public static T GetService<T>() where T : SceneService
        {
            var core = GetSceneCore();
            foreach (var service in core.Services.Values)
            {
                if (service.GetType() == typeof(T))
                    return (T)service;
            }

            Debug.LogWarning($"No services found in the scene");
            return null;
        }
    }    
}