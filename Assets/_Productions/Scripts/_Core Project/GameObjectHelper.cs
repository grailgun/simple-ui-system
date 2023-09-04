using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ForgeFun.AvarikSaga.Helper
{
    public static class GameObjectHelper
    {
        public static T GetComponentInTag<T>(string tagName = "GameDependency")
        {
            var gos = GameObject.FindGameObjectsWithTag(tagName);
            if (gos.Length > 0) 
                return GetComponentFromGameObjects<T>(gos);
            
            Debug.Log("no gameobjects is found with tag of " + tagName);
            return default;
        }

        public static T GetComponentFromGameObjects<T>(GameObject[] gos)
        {
            for (int i = 0; i < gos.Length; i++)
            {
                var component = gos[i].GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }

            Debug.Log("component not found");
            return default;
        }

        public static T GetOrAddComponent<T>(this MonoBehaviour mb) where T : MonoBehaviour
        {
            var res = mb.GetComponent<T>();
            if (res == null)
                res = mb.AddComponent<T>();

            return res;
        }
    }
}