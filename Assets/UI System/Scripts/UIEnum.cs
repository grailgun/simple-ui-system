using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    [CreateAssetMenu(menuName = "UI Manager/UIEnum")]
    public class UIEnum : ScriptableObject
    {
    }

    public static class UIEnumExtension
    {
        public static bool IsEqual(this UIEnum origin, UIEnum comparer)
        {
            return origin.name == comparer.name;
        }
    }
}