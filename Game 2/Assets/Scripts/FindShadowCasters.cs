using UnityEngine;
using UnityEditor;

public class FindShadowCasters
{
    [MenuItem("Tools/List Shadow Casters")]
    static void ListShadowCasters()
    {
        var renderers = Object.FindObjectsOfType<MeshRenderer>();
        int count = 0;

        foreach (var r in renderers)
        {
            if (r.shadowCastingMode != UnityEngine.Rendering.ShadowCastingMode.Off)
            {
                Debug.Log($"Shadow caster: {r.name}", r.gameObject);
                count++;
            }
        }

        Debug.Log($"Total shadow-casting objects: {count}");
    }
}
