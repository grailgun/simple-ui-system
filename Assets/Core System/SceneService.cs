using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public abstract class SceneService : MonoBehaviour
    {
        public EnumId ServiceId => serviceId;
        public SceneCore Core => _sceneCore;

        [Header("SERVICE PARAMETER")]
        [SerializeField]
        private EnumId serviceId;

        public UnityEvent OnServiceInitialized;

        private SceneCore _sceneCore;

        internal void InjectCore(SceneCore sceneCore)
        {
            _sceneCore = sceneCore;
        }

        internal void Init()
        {
            OnServiceInitialized?.Invoke();
        }

        public virtual IEnumerator StartService() { yield return null; }
    }
}