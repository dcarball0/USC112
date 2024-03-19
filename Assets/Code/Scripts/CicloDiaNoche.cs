using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteAlways]
public class CicloDiaNoche : MonoBehaviour
{
    // Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    // Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;
    //public float timeScale = 1f;
    private float[] timeScales = { 0.5f, 1f, 2f, 4f, 8f };
    private int currentIndex = 1; // Start at index 1 (1x timescale)

    // Se elimina la lista de farolas ya que no las controlaremos directamente desde aqui

    // Anade una propiedad publica para saber si es de noche
    public bool IsNight => TimeOfDay < 12 || TimeOfDay > 16; // Asume noche entre las 18:00 y las 6:00

    public void ChangeTimeScale(ClickEvent evt)
    {
        currentIndex = (currentIndex + 1) % timeScales.Length;
        Time.timeScale = timeScales[currentIndex];
        

        Button b = (Button)evt.target;
        // Update button text to show the new timescale
        b.text = timeScales[currentIndex] + "x";
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // (Reemplazar con referencia al tiempo del juego si es necesario)
            TimeOfDay += Time.deltaTime * Time.timeScale;
            TimeOfDay %= 24; // Modulo para asegurar siempre entre 0-24
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

        // Si la luz direccional esta establecida, entonces rotarla y configurar su color
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

        // Buscar en la configuracion de renderizado la luz del sol
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
