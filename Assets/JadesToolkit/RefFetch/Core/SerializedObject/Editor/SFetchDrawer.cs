using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SFetchAttribute))]
public class SFetchDrawer : PropertyDrawer
{
    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
        //base.OnGUI(position, property, label);
        //SFetchAttribute fetchAttribute = attribute as SFetchAttribute;
        //var type = fetchAttribute.ObjectType;
        //var so = property.serializedObject;

        //if(property.objectReferenceValue == null)
        //{
        //    var gameObject = so.targetObject as GameObject;
        //    var obj = (so.targetObject as GameObject).GetComponent(type);
        //    if (obj == null && fetchAttribute.AddComponent)
        //        obj = gameObject.AddComponent(type);
        //    if(obj != null)
        //        property.objectReferenceValue = obj;

        //    so.ApplyModifiedProperties();
        //}

    //}
}
