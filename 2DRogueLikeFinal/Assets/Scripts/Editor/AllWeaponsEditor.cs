using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllWeapons))]
public class AllWeaponsEditor : Editor
{
    private const string creationPath = "Assets/Resources/AllWeapons.asset";

    [MenuItem("Assets/Create/AllWeapons")]
    private static void CreateAllWeaponsAsset()
    {
        if (AllWeapons.Instance)
            return;

        AllWeapons instance = CreateInstance<AllWeapons>();
        AllWeapons.Instance = instance;                
        AssetDatabase.CreateAsset(instance, creationPath);

    }
}
