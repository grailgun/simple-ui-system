using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForgeFun.AvarikSaga.Helper
{
    public class ContextProvider : MonoBehaviour
    {
        public GameObject[] contextObjects;

        public static T GetContextFromParent<T>(MonoBehaviour mb)
        {
            var context = mb.GetComponentInParent<ContextProvider>();
            if (context != null)
            {
                return GameObjectHelper.GetComponentFromGameObjects<T>(context.contextObjects);
            }

            Debug.Log("Context Provider not found");
            return default;
        }

        public static GameObject GetContextWithName(GameObject go, string goName)
        {
            var contextProvider = go.GetComponent<ContextProvider>();
            if (contextProvider == null)
            {
                Debug.Log("No ContextProvider found in this gameObject");
                return null;
            }

            var result = contextProvider.contextObjects.FirstOrDefault(x => x.name == goName);
            if (result != null)
            {
                return result;
            }

            Debug.Log("GameObject Not Found");
            return null;
        }
    }
}