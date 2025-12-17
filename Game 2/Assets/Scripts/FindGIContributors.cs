using UnityEngine;
using UnityEditor;

public class FindGIContributors : EditorWindow
{
    [MenuItem("Tools/GI/Find GI Contributors")]
    public static void ShowWindow()
    {
        GetWindow<FindGIContributors>("GI Contributors");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Scan Scene"))
        {
            ScanScene();
        }
    }

    private void ScanScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            if (!go.activeInHierarchy)
                continue;

            // Must have a Renderer to contribute to GI
            if (!go.TryGetComponent<Renderer>(out _))
                continue;

            // Check the actual "Contribute GI" flag
            if (GameObjectUtility.AreStaticEditorFlagsSet(
                go,
                StaticEditorFlags.ContributeGI))
            {
                Debug.Log($"GI Contributor: {go.name}", go);
                count++;
            }
        }

        Debug.Log($"Scan complete. Found {count} GI-contributing objects.");
    }
}
