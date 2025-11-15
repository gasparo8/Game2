using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SingleUseMaterials : EditorWindow
{
    [MenuItem("Tools/Find Single-Use Materials")]
    static void ShowWindow()
    {
        GetWindow<SingleUseMaterials>("Single-Use Materials");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Scan Scene"))
        {
            FindAndHighlightSingleUseMaterials();
        }
    }

    static void FindAndHighlightSingleUseMaterials()
    {
        // Dictionary to track which GameObjects use each material
        Dictionary<Material, List<GameObject>> materialUsage = new Dictionary<Material, List<GameObject>>();

        // Find all renderers in the scene
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();

        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.sharedMaterials)
            {
                if (mat == null) continue;

                if (!materialUsage.ContainsKey(mat))
                    materialUsage[mat] = new List<GameObject>();

                if (!materialUsage[mat].Contains(rend.gameObject))
                    materialUsage[mat].Add(rend.gameObject);
            }
        }

        // Find materials used only once
        List<GameObject> objectsToSelect = new List<GameObject>();
        Debug.Log("=== Single-Use Materials ===");

        foreach (var kvp in materialUsage)
        {
            if (kvp.Value.Count == 1)
            {
                Material singleMat = kvp.Key;
                GameObject obj = kvp.Value[0];
                objectsToSelect.Add(obj);

                //  Clicking this log entry now highlights the object in Hierarchy
                Debug.Log(
                    $"Material '{singleMat.name}' is only used by GameObject '{obj.name}'",
                    obj // <-- context object enables ping/select
                );
            }
        }

        // Select all objects in the Hierarchy
        if (objectsToSelect.Count > 0)
        {
            Selection.objects = objectsToSelect.ToArray();
            Debug.Log($"Selected {objectsToSelect.Count} object(s) using single-use materials.");
        }
        else
        {
            Debug.Log("No single-use materials found in this scene.");
        }

        Debug.Log("=== Scan Complete ===");
    }
}
