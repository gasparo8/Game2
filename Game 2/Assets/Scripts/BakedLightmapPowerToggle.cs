using UnityEngine;
using UnityEngine.Rendering;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class BakedLightmapPowerToggle : MonoBehaviour
{
    // Original baked lightmaps
    private LightmapData[] bakedLightmaps;
    private LightmapsMode bakedMode;

    // Real-time lights
    private Light[] sceneLights;
    private bool[] lightsInitialState;

    // Ambient lighting
    private AmbientMode originalAmbientMode;
    private Color originalAmbientColor;
    private float originalAmbientIntensity;

    private Material originalSkybox;

    private bool cached;

    private void Awake()
    {
        CacheBakedLighting();
        CacheSceneLights();
        CacheAmbientSettings();
    }

    private void CacheBakedLighting()
    {
        if (cached)
            return;

        bakedLightmaps = LightmapSettings.lightmaps;
        bakedMode = LightmapSettings.lightmapsMode;

        cached = true;
    }

    private void CacheSceneLights()
    {
        sceneLights = FindObjectsOfType<Light>();
        lightsInitialState = new bool[sceneLights.Length];
        for (int i = 0; i < sceneLights.Length; i++)
        {
            lightsInitialState[i] = sceneLights[i].enabled;
        }
    }

    private void CacheAmbientSettings()
    {
        originalAmbientMode = RenderSettings.ambientMode;
        originalAmbientColor = RenderSettings.ambientLight;
        originalAmbientIntensity = RenderSettings.ambientIntensity;
        originalSkybox = RenderSettings.skybox;
    }

    public void SimulatePowerOutage()
    {
        CacheBakedLighting();
        CacheSceneLights();
        CacheAmbientSettings();

        // Remove baked lighting
        LightmapSettings.lightmaps = new LightmapData[0];
        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;

        // Turn off all real-time lights
        foreach (var light in sceneLights)
            light.enabled = false;

        // Remove ambient lighting
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.ambientLight = Color.black;

        // Optionally, remove skybox
        RenderSettings.skybox = null;
    }

    public void RestoreBakedLighting()
    {
        if (!cached)
            return;

        // Restore baked lighting
        LightmapSettings.lightmaps = bakedLightmaps;
        LightmapSettings.lightmapsMode = bakedMode;

        // Restore real-time lights
        for (int i = 0; i < sceneLights.Length; i++)
        {
            if (sceneLights[i] != null)
                sceneLights[i].enabled = lightsInitialState[i];
        }

        // Restore ambient settings
        RenderSettings.ambientMode = originalAmbientMode;
        RenderSettings.ambientLight = originalAmbientColor;
        RenderSettings.ambientIntensity = originalAmbientIntensity;
        RenderSettings.skybox = originalSkybox;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BakedLightmapPowerToggle))]
public class BakedLightmapPowerToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BakedLightmapPowerToggle toggle = (BakedLightmapPowerToggle)target;

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
