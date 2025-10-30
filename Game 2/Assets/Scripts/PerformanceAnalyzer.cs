using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PerformanceAnalyzer : EditorWindow
{
    int topCount = 10;
    bool includeInactive = false;

    [MenuItem("Tools/Performance Analyzer")]
    static void Init() => GetWindow<PerformanceAnalyzer>("Performance Analyzer");

    void OnGUI()
    {
        GUILayout.Label("Scene Performance Analyzer", EditorStyles.boldLabel);
        topCount = EditorGUILayout.IntField("Show Top N", topCount);
        includeInactive = EditorGUILayout.Toggle("Include Inactive", includeInactive);

        if (GUILayout.Button("Scan Scene"))
            ScanScene();
    }

    void ScanScene()
    {
        var entries = new List<(GameObject go, string meshName, int verts, int tris, int materials, bool castsShadows, bool skinned, float score)>();
        var roots = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (var root in roots)
        {
            // Mesh Filters
            var mfs = root.GetComponentsInChildren<MeshFilter>(includeInactive);
            foreach (var mf in mfs)
            {
                if (mf.sharedMesh == null) continue;
                var renderer = mf.GetComponent<MeshRenderer>();
                if (renderer == null) continue;

                var mesh = mf.sharedMesh;
                int tris = mesh.triangles.Length / 3;
                int verts = mesh.vertexCount;
                int mats = renderer.sharedMaterials.Length;
                bool shadows = renderer.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
                bool skinned = false;

                float score = CalculateScore(tris, mats, shadows, skinned);
                entries.Add((mf.gameObject, mesh.name, verts, tris, mats, shadows, skinned, score));
            }

            // Skinned Mesh Renderers
            var smrs = root.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive);
            foreach (var smr in smrs)
            {
                if (smr.sharedMesh == null) continue;
                var mesh = smr.sharedMesh;
                int tris = mesh.triangles.Length / 3;
                int verts = mesh.vertexCount;
                int mats = smr.sharedMaterials.Length;
                bool shadows = smr.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off;
                bool skinned = true;

                float score = CalculateScore(tris, mats, shadows, skinned);
                entries.Add((smr.gameObject, mesh.name, verts, tris, mats, shadows, skinned, score));
            }
        }

        if (entries.Count == 0)
        {
            Debug.Log("No meshes found in scene!");
            return;
        }

        var sorted = entries.OrderByDescending(x => x.score).Take(topCount).ToList();

        Debug.Log($"--- Top {topCount} Performance-Heavy Objects ---");
        for (int i = 0; i < sorted.Count; i++)
        {
            var e = sorted[i];
            Debug.Log(
                $"{i + 1}. {e.go.name} — Score: {e.score:F1} | {e.tris:N0} tris | {e.verts:N0} verts | {e.materials} mats | Shadows: {e.castsShadows} | Skinned: {e.skinned}",
                e.go  // makes the line item clickable in the Console
            );
        }

        Debug.Log("Click any line above to highlight that object in the Hierarchy.");
    }

    float CalculateScore(int tris, int mats, bool shadows, bool skinned)
    {
        // Weighted scoring system (tweakable)
        float score = 0;
        score += tris * 0.01f;        // geometry cost
        score += mats * 100f;         // draw call/material cost
        if (shadows) score += 200f;   // shadow rendering
        if (skinned) score += 300f;   // skinning cost
        return score;
    }
}
