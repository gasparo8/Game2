using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MaterialCountFinder : EditorWindow
{
    [MenuItem("Tools/Find Objects by Material Count")]
    static void Init()
    {
        // Find all MeshRenderers in the scene
        var renderers = GameObject.FindObjectsOfType<MeshRenderer>();

        // Create a list of pairs (renderer, materialCount)
        var renderList = new List<(MeshRenderer renderer, int count)>();

        foreach (var renderer in renderers)
        {
            int matCount = renderer.sharedMaterials.Length;
            renderList.Add((renderer, matCount));
        }

        // Sort descending by material count
        var sorted = renderList.OrderByDescending(r => r.count);

        // Print results to console
        Debug.Log("=== Objects Sorted by Material Count (Highest First) ===");
        foreach (var r in sorted)
        {
            Debug.Log($"{r.renderer.gameObject.name} — {r.count} materials", r.renderer.gameObject);
        }
    }
}
