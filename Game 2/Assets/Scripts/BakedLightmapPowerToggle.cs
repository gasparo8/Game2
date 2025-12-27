using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BakedLightmapPowerToggle: MonoBehaviour
{
    private LightmapData[] bakedLightmaps;
    private LightmapsMode bakedMode;
    private bool cached;

    private void Awake()
    {
        CacheBakedLighting();
    }

    private void CacheBakedLighting()
    {
        if (cached)
            return;

        bakedLightmaps = LightmapSettings.lightmaps;
        bakedMode = LightmapSettings.lightmapsMode;
        cached = true;
    }

    public void SimulatePowerOutage()
    {
        CacheBakedLighting();

        LightmapSettings.lightmaps = new LightmapData[0];
        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
    }

    public void RestoreBakedLighting()
    {
        if (!cached)
            return;

        LightmapSettings.lightmaps = bakedLightmaps;
        LightmapSettings.lightmapsMode = bakedMode;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BakedLightmapPowerToggle))]
public class BakedLightmapPowerToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BakedLightmapPowerToggle toggle =
            (BakedLightmapPowerToggle)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Simulate Power Outage"))
        {
            toggle.SimulatePowerOutage();
        }

        if (GUILayout.Button("Restore Baked Lighting"))
        {
            toggle.RestoreBakedLighting();
        }
    }
}
#endif
