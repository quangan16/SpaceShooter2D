using UnityEngine;
using UnityEditor;

public class Example 
{
    [MenuItem("Examples/Instantiate Selected")]
    public static void InstantiatePrefab()
    {
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject);
    }

    [MenuItem("Examples/Instantiate Selected", true)]
    public static bool ValidateInstantiatePrefab()
    {
        GameObject go = Selection.activeObject as GameObject;
        if (go == null)
            return false;

        return PrefabUtility.IsPartOfPrefabAsset(go);
    }
}