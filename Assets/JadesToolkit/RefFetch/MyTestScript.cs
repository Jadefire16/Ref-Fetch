using JadesToolkit;
using UnityEngine;

[Manageable]
public class MyTestScript : MonoBehaviour
{
    [AutoAssignable(typeof(MeshRenderer), true)]
    public MeshRenderer col;

    [AutoAssignable(typeof(MeshRenderer))]
    public MeshRenderer Col { get; }

    [ContextMenu("Log")]
    public void LogValues()
    {
        Debug.Log($"Field {col}");
        Debug.Log($"Property {Col}");
    }

}
