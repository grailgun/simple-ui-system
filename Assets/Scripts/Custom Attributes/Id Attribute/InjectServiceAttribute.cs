using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class IService<T> where T : SceneService
{
    public EnumId ServiceId;

    [HideInInspector]
    public T Service;
}
