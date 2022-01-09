using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

public class ComponentFinder : EditorWindow
{
    private string currentTypeName = "Rigidbody";

    private Type currentType;

    [MenuItem("Jades Toolkit/Test")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ComponentFinder));
    }

    void OnGUI()
    {
        currentTypeName = EditorGUILayout.TextField(currentTypeName);

        if (GUILayout.Button("Search"))
        {
            var activeObjs = UnityEngine.Object.FindObjectsOfType<GameObject>() as GameObject[];
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                var types = assemblies[i].GetTypes();
                if (types.Length > 0)
                {
                    for (int j = 0; j < types.Length; j++)
                    {
                        if (types[j].Name == currentTypeName)
                            currentType = types[j];
                    }
                }    
            }

            for (int i = 0; i < activeObjs.Length; i++)
            {
                var comps = activeObjs[i].GetComponentsInChildren(currentType);
                for (int j = 0; j < comps.Length; j++)
                {
                    Debug.Log(comps[j], activeObjs[i]);
                }
            }

        }

    }
}
