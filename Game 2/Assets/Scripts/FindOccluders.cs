using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class FindOccluders
{
    [MenuItem("Tools/Occlusion/Log Occluder Static Objects")]
    public static void LogOccluders()
    {
        var occluders = GetOccluderObjectsInActiveScene();
        Debug.Log($"Found {occluders.Count} Occluder Static object(s).");

        foreach (var go in occluders)
        {
            // Passing 'go' as the context makes the Console entry clickable
            Debug.Log($"Occluder: {GetHierarchyPath(go)}", go);
        }
    }

    [MenuItem("Tools/Occlusion/Select Occluder Static Objects")]
    public static void SelectOccluders()
    {
        var occluders = GetOccluderObjectsInActiveScene();
        Selection.objects = occluders.ToArray();

        Debug.Log($"Selected {occluders.Count} Occluder Static object(s).");

        if (occluders.Count > 0)
            EditorGUIUtility.PingObject(occluders[0]);
    }

    // ------------ Helpers ------------

    static List<GameObject> GetOccluderObjectsInActiveScene()
    {
        List<GameObject> found = new List<GameObject>();

        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid()) return found;

        foreach (var root in scene.GetRootGameObjects())
            Scan(root, found);

        return found;
    }

    static void Scan(GameObject go, List<GameObject> list)
    {
        var flags = GameObjectUtility.GetStaticEditorFlags(go);

        if ((flags & StaticEditorFlags.OccluderStatic) != 0)
            list.Add(go);

        for (int i = 0; i < go.transform.childCount; i++)
            Scan(go.transform.GetChild(i).gameObject, list);
    }

    static string GetHierarchyPath(GameObject go)
    {
        string path = go.name;
        Transform t = go.transform;

        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }

        return path;
    }
}
