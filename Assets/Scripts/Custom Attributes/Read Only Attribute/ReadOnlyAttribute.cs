using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomAttribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}