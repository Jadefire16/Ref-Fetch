using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFetchAttribute : PropertyAttribute
{
    public Type ObjectType { get; private set; }
    public bool AddComponent { get; private set; } = false;
    public SFetchAttribute(Type objectType)
    {
        ObjectType = objectType;
    }
    public SFetchAttribute(Type objectType, bool addComponent) : this(objectType)
    {
        AddComponent = addComponent;
    }
}
