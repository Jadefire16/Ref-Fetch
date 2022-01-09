using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

public class ComponentFetchTool : EditorWindow
{
    private List<Type> types = new List<Type>();
    private Dictionary<string, Type> typeDictionary = new Dictionary<string, Type>();

    private List<Assembly> assemblies = new List<Assembly>();

    private Type type;

    private void Awake()
    {
        assemblies.Clear();
        types.Clear();
        typeDictionary.Clear();


        if (!assemblies.Contains(Assembly.GetAssembly(typeof(Rigidbody))))
            assemblies.Add(Assembly.GetAssembly(typeof(Rigidbody)));
        if (!assemblies.Contains(Assembly.GetAssembly(typeof(Component))))
            assemblies.Add(Assembly.GetAssembly(typeof(Component)));
        if (!assemblies.Contains(Assembly.GetAssembly(typeof(MonoBehaviour))))
            assemblies.Add(Assembly.GetAssembly(typeof(MonoBehaviour)));
        if (!assemblies.Contains(Assembly.GetAssembly(typeof(MonoScript))))
            assemblies.Add(Assembly.GetAssembly(typeof(MonoScript)));
        if (!assemblies.Contains(Assembly.GetAssembly(typeof(Collider))))
            assemblies.Add(Assembly.GetAssembly(typeof(Collider)));
        if (!assemblies.Contains(Assembly.GetAssembly(typeof(Renderer))))
            assemblies.Add(Assembly.GetAssembly(typeof(Renderer)));

        for (int i = 0; i < assemblies.Count; i++)
        {
            var allTypes = assemblies[i].GetTypes().Where(t => (t.IsSubclassOf(typeof(Component)) || t.IsSubclassOf(typeof(MonoBehaviour))) && t.IsAbstract == false).ToArray();
            Debug.Log(allTypes.Length);
            for (int j = 0; j < allTypes.Length; j++)
            {
                if (types.Contains(allTypes[j]))
                {
                    Debug.Log("Continued");
                    continue;
                }
                types.Add(allTypes[j]);
                if (types.Contains(allTypes[j]))
                    typeDictionary.Add(allTypes[j].Name, allTypes[j]);
                Debug.Log(allTypes[j].Name);
            }
        }
    }

    [MenuItem("Jades Toolkit/Test")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ComponentFetchTool));
    }

    void OnGUI()
    {
        int selected = 0;
        List<string> options = new List<string>(typeDictionary.Keys.Count);
        foreach (var key in typeDictionary.Keys)
        { // convert to for loop later
            options.Add(key);
        }
        options.Sort();
        selected = EditorGUILayout.Popup("Label", selected, options.ToArray());
    }

    Type GetType(string name)
    {
        typeDictionary.TryGetValue(name.ToLower(), out Type t);
        if (t == null)
            throw new KeyNotFoundException($"Type {name} was not found!");
        return t;
    }

}
//GameObject[] objs;
//var transforms = Selection.transforms;
//objs = new GameObject[transforms.Length];
//for (int i = 0; i < transforms.Length; i++)
//    objs[i] = transforms[i].gameObject;
//List<GameObject> selected = new List<GameObject>(objs.Length);
//for (int i = 0; i < selected.Count; i++)
//{
//    if (objs[i].GetComponent(type) != null)
//        selected.Add(objs[i]);
//}
//Selection.objects = selected.ToArray();