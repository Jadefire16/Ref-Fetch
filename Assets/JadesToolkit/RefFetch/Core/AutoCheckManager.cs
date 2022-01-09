using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace JadesToolkit
{
    public static class AutoCheckManager
    {
        [MenuItem("Jades Toolkit/Auto Check/Null Check")]
        private static void NullCheckFields()
        {
            //Assembly assembly = Assembly.GetAssembly(typeof(MonoBehaviour));
            var activeBehaviours = UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

            for (int i = 0; i < activeBehaviours.Length; i++)
            {
                var type = activeBehaviours[i].GetType();
                if (!type.IsDefined(typeof(ManageableAttribute)))
                    continue;
                CheckFields(type.GetFields(), activeBehaviours[i]);
            }
        }

        private static void CheckFields(FieldInfo[] fieldInfos, MonoBehaviour behaviour)
        {
            if (fieldInfos.Length <= 0)
                return;
            FieldInfo info;
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                info = fieldInfos[i];
                if (!info.FieldType.IsSubclassOf(typeof(Component)))
                    continue;
                UnityEngine.Object obj = (UnityEngine.Object)info.GetValue(behaviour);
                if(obj == null)
                    Debug.Log($"Object {behaviour.gameObject}\nValue '{info.Name}' is null!", behaviour.gameObject);
            }
        }

        private static void CheckForComponent<T>() where T : Component
        {
            //var selectedBehaviours = Selection.

            //for (int i = 0; i < activeBehaviours; i++)
            //{

            //}
        }


    }
}