using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    // Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    // Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    // Se elimina la lista de farolas ya que no las controlaremos directamente desde aquí

    // Añade una propiedad pública para saber si es de noche
    public bool IsNight => TimeOfDay < 6 || TimeOfDay > 18; // Asume noche entre las 18:00 y las 6:00

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // (Reemplazar con referencia al tiempo del juego si es necesario)
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; // Módulo para asegurar siempre entre 0-24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        // Configurar luz ambiental y niebla
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        // Si la luz direccional está establecida, entonces rotarla y configurar su color
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    // Intenta encontrar una luz direccional para usar si no hemos configurado una
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        // Buscar en la configuración de renderizado la luz del sol
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        // Buscar en la escena una luz que cumpla los criterios (direccional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
