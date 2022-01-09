using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class AssemblyFetchService
{
    private static Dictionary<Assembly, List<Type>> unityAssemblies = new Dictionary<Assembly, List<Type>>(8);

    [SerializeField]
    private static bool initialized = false;

    [InitializeOnLoadMethod]
    public static void InitializeOnLoad()
    {
        Initialize(false);
    }

    private static void Initialize(bool force = false)
    {
        Debug.Log("Initializing Fetch Services...");
        if (initialized && !force)
        {
            Debug.Log("Already Initialized!");
            return;
        }

        var allAssemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < allAssemblies.Length; i++)
        {
            var allTypes = allAssemblies[i].GetTypes().Where(t => t.IsAssignableFrom(typeof(UnityEngine.Object)) || t.IsSubclassOf(typeof(UnityEngine.Object))).ToList();
            if (allTypes.Count > 0)
            {
                if (!unityAssemblies.ContainsKey(allAssemblies[i]))
                {
                    unityAssemblies.Add(allAssemblies[i], allTypes);
                    //AssemblyStorage s = new AssemblyStorage(allAssemblies[i]);
                    //Debug.Log(s.hint);
                    for (int j = 0; j < allTypes.Count; j++)
                    {
                        Debug.Log(allTypes[j].Name);
                    }


                }
            }
        }
        Debug.Log($"{unityAssemblies.Count} Assemblies loaded\n {nameof(AssemblyFetchService)} is now initialized");
        initialized = true;   
    }

    //public static Type FetchType(string typeName)
    //{

    //}

    private class AssemblyStorage
    {
        public Assembly assembly { get; private set; }
        public string hint { get; private set; }

        public AssemblyStorage(Assembly assembly)
        {
            this.assembly = assembly;
            AssemblyName asmName = assembly.GetName();



        }
    }

    private static List<int> GetCharPositions(this string value, char c)
    {
        List<int> positions = new List<int>();
        char[] chars = value.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i].Equals(c))
                positions.Add(i);
        }
        return positions;
    }

}
//for (int i = 0; i < assemblies.Length; i++)
//{
//    if (!types.TryGetValue(assemblies[i], out List<Type> t))
//    {
//        Debug.Log($"Assembly {assemblies[i]} doesnt exist");
//        continue;
//    }
//    if (t == null)
//    {


//        Debug.Log("Null");
//        continue;
//    }
//    for (int j = 0; j < t.Count; j++)
//    {
//        Debug.Log(t[j]);
//    }
//}
//Debug.Log(types.Count);