using JadesToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace JadesToolkit
{
    public static class AutoAssignableManager
    {
        [MenuItem("Jades Toolkit/Auto Assign/Fields")]
        private static void AutoAssignFields()
        {
            //Assembly assembly = Assembly.GetAssembly(typeof(MonoBehaviour));
            var activeBehaviours = UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

            for (int i = 0; i < activeBehaviours.Length; i++)
            {
                var type = activeBehaviours[i].GetType();
                if (!type.IsDefined(typeof(ManageableAttribute)))
                    continue;
                AssignFields(type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), activeBehaviours[i]);
            }
        }


        [MenuItem("Jades Toolkit/Auto Assign/Properties")]
        private static void AutoAssignProperties()
        {
            var activeBehaviours = UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

            for (int i = 0; i < activeBehaviours.Length; i++)
            {
                var type = activeBehaviours[i].GetType();
                if (!type.IsDefined(typeof(ManageableAttribute)))
                    continue;
                AssignProperties(type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance), activeBehaviours[i]);
            }
        }

        /// <summary>
        /// Attempts to find and assign values to marked fields if they are null
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="behaviour"></param>
        private static void AssignFields(FieldInfo[] fields, MonoBehaviour behaviour)
        {
            if (fields.Length <= 0)
            {
                Debug.Log("No Fields");
                return;
            }
            FieldInfo info;
            for (int i = 0; i < fields.Length; i++)
            {
                info = fields[i];
                if (info.IsStatic || info.IsLiteral)
                {
                    Debug.LogWarning($"{nameof(AutoAssignableAttribute)} may not be applied to static, readonly or const values!", behaviour.gameObject);
                    continue;
                }            
                if (!(info.GetCustomAttribute(typeof(AutoAssignableAttribute)) is AutoAssignableAttribute attribute))
                    continue;
                if (info.IsInitOnly)
                {
                    Debug.LogWarning($"{nameof(AutoAssignableAttribute)} may not be applied to readonly values!", behaviour.gameObject);
                    continue;
                }
                if (info.IsPrivate)
                {
                    if (!(info.GetCustomAttribute(typeof(SerializeField)) is SerializeField serialized))
                    {
                        Debug.LogWarning($"{nameof(AutoAssignableAttribute)} may not be applied to private fields not marked with [SerializeField]");
                        continue;
                    }
                }        
                if (!info.FieldType.IsSubclassOf(typeof(Component)))
                {
                    Debug.LogError($"Field '{info.Name}' must derive from {nameof(UnityEngine.Component)} \n" +
                        $"Behaviour: {behaviour}");
                    continue;
                }
                UnityEngine.Object obj = (UnityEngine.Object)info.GetValue(behaviour);
                if (obj == null)
                {
                    var component = behaviour.gameObject.GetComponent(attribute.AttributeType);
                    if (component != null)
                    {
                        info.SetValue(behaviour, component);
                        continue;
                    }
                    if (attribute.AddIfNotFound)
                    {
                        component = Undo.AddComponent(behaviour.gameObject, attribute.AttributeType);
                        info.SetValue(behaviour, component);
                        Debug.Log($"Value was not found on {behaviour.gameObject} \n Component of type {attribute.AttributeType} was added to the object\nThe value '{info.Name}' was assigned to an added component on behaviour {behaviour}", behaviour);
                    }
                }
            }
        }

        /// <summary>
        /// Attempts to find and assign values to marked properties if they are null
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="behaviour"></param>
        private static void AssignProperties(PropertyInfo[] properties, MonoBehaviour behaviour)
        {
            if (properties.Length <= 0)
                return;

            PropertyInfo info;
            for (int i = 0; i < properties.Length; i++)
            {
                info = properties[i];
                if (!(info.GetCustomAttribute(typeof(AutoAssignableAttribute)) is AutoAssignableAttribute attribute))
                    continue;
                if (!info.PropertyType.IsSubclassOf(typeof(Component)))
                {
                    Debug.LogError($"Field '{info.Name}' must derive from {nameof(UnityEngine.Component)} \n" +
                        $"Behaviour: {behaviour}");
                    continue;
                }
                if (info.SetMethod == null || !info.CanWrite)
                    continue;

                UnityEngine.Object obj = (UnityEngine.Object)info.GetValue(behaviour);
                if (obj == null)
                {
                    var component = behaviour.gameObject.GetComponent(attribute.AttributeType);
                    if (component != null)
                    {
                        info.SetValue(behaviour, component);
                        continue;
                    }
                    if (attribute.AddIfNotFound)
                    {
                        component = Undo.AddComponent(behaviour.gameObject, attribute.AttributeType);
                        info.SetValue(behaviour, component);
                        Debug.Log($"Value was not found on {behaviour.gameObject} \n Component of type {attribute.AttributeType} was added to the object\nThe value '{info.Name}' was assigned to an added component on behaviour {behaviour}", behaviour);
                    }
                }
            }
        }
    }
}



///Todo
///