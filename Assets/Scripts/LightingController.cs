using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class LightingController : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;
    [SerializeField]
    private LightingPreset LightingPreset;
    [SerializeField, Range(0, 24)]
    private float TimeOfDay;
    private readonly float UpdateInterval= 15f;

    private void Start()
    {
        if (Application.isPlaying)
        {
            StartCoroutine(SendUpdate());
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null) return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            foreach (Light light in GameObject.FindObjectsOfType<Light>())
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = LightingPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = LightingPreset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = LightingPreset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void Update()
    {
        if (LightingPreset == null) return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * 0.05f;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private IEnumerator SendUpdate()
    {
        for(;;)
        {
            GameEvents.instance.TimeOfDayChanged(TimeOfDay);
            yield return new WaitForSeconds(UpdateInterval);
        }
    }
}
