using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class AssemblyReference
{
    public static List<string> assemblies;
    [ListToPopup(typeof(AssemblyReference), "assemblies")]
    public string assembly;
}

public class UserAssemblyProvider : ScriptableObject
{
    [Tooltip("Add assemblies that contains your View Models")]
    [SerializeField]
    private List<AssemblyReference> assemblyReferences;

    public List<Assembly> Assemblies
    {
        get
        {
            if (assemblyReferences != null)
            {
                var assemblies = new List<Assembly>();
                assemblyReferences.ForEach( 
                    a => assemblies.Add(Assembly.Load(a.assembly)));

                if (assemblies.Count != 0) return assemblies;
            }
            else
            {
                assemblyReferences = new List<AssemblyReference>() {
                    new AssemblyReference() {assembly = "Assembly-CSharp" } };
            }

            return new List<Assembly>() { Assembly.Load("Assembly-CSharp") };
        }
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (AssemblyReference.assemblies == null)
            CollectPopupAssemblies();
#endif
    }

#if UNITY_EDITOR
    public void CollectPopupAssemblies()
    {
        AssemblyReference.assemblies?.Clear();
        AssemblyReference.assemblies = new List<string>();

        UnityEditor.Compilation.Assembly[] playerAssemblies =
            UnityEditor.Compilation.CompilationPipeline
            .GetAssemblies(UnityEditor.Compilation.AssembliesType.Player);

        foreach (var assembly in playerAssemblies)
        {
            AssemblyReference.assemblies.Add(assembly.name);
        }
    }
#endif

    public static UserAssemblyProvider LoadOrCreate()
    {
        string assetName = "UserAssemblyProvider";
        var provider = Resources.Load<UserAssemblyProvider>(assetName);

        if (provider != null) return provider;

#if UNITY_EDITOR
        string resources = "Assets/Unity-MVVM/Resources";

        if (!Directory.Exists(resources))
            Directory.CreateDirectory(resources);

        provider = (UserAssemblyProvider)CreateInstance(typeof(UserAssemblyProvider));
        UnityEditor.AssetDatabase.CreateAsset(provider, $"{resources}/{assetName}.asset");
#endif

        return provider;
    }
}
