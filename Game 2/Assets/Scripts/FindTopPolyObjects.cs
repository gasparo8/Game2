using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class FindTopPolyObjects : EditorWindow
{
    int topCount = 10;
    bool includeInactive = false;

    [MenuItem("Tools/Find Top Poly Objects")]
    static void Init()
    {
        GetWindow<FindTopPolyObjects>("Top Poly Finder");
    }

    void OnGUI()
    {
        GUILayout.Label("Find Top Poly Objects", EditorStyles.boldLabel);
        topCount = EditorGUILayout.IntField("Show Top N Objects", topCount);
        includeInactive = EditorGUILayout.Toggle("Include Inactive", includeInactive);

        if (GUILayout.Button("Scan Scene"))
            ScanScene();
    }

    void ScanScene()
    {
        MeshFilter[] meshFilters = includeInactive ?
            Resources.FindObjectsOfTypeAll<MeshFilter>() :
            FindObjectsOfType<MeshFilter>();

        var meshData = new List<(string name, int verts, GameObject go)>();

        foreach (MeshFilter mf in meshFilters)
        {
            if (mf.sharedMesh == null) continue;
            meshData.Add((mf.name, mf.sharedMesh.vertexCount, mf.gameObject));
        }

        if (meshData.Count == 0)
        {
            Debug.Log("No MeshFilters found in scene!");
            return;
        }

        // Sort by vertex count descending
        var topMeshes = meshData.OrderByDescending(x => x.verts).Take(topCount).ToList();

        Debug.Log($"Top {topMeshes.Count} Meshes by Vertex Count:");

        for (int i = 0; i < topMeshes.Count; i++)
        {
            var m = topMeshes[i];
            Debug.Log($"{i + 1}. {m.name} — {m.verts} vertices", m.go);
        }

        Debug.Log("Click any entry above to select that object in the scene.");
    }
}
