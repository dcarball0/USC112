using UnityEngine;
/*
public class StreetLightControl : MonoBehaviour
{
    public LightingManager lightingManager; // Referencia al LightingManager
    private Light myLight; // Componente Light de esta farola
    public Transform bus; // Referencia al Transform del bus
    public float detectionRange = 10f; // Rango para detectar el bus

    void Start()
    {
        myLight = GetComponentInChildren<Light>(); // Asume que la luz es un hijo de la farola
        myLight.enabled = false; // Asegura que la luz está apagada inicialmente
    }

    void Update()
    {
        if (lightingManager.IsNight && IsBusClose())
        {
            myLight.enabled = true; // Enciende la luz si es de noche y el bus está cerca
        }
        else
        {
            myLight.enabled = false; // De lo contrario, asegura que la luz esté apagada
        }
    }

    bool IsBusClose()
    {
        if (bus != null)
        {
            float distanceToBus = Vector3.Distance(transform.position, bus.position);
            return distanceToBus <= detectionRange;
        }
        return false;
    }
}
*/