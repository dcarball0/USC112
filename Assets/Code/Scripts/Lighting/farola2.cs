using UnityEngine;
using System.Collections; // Necesario para usar corutinas

public class StreetLightControl : MonoBehaviour
{
    private Light myLight;
    public float delayBeforeTurningOff = 0.5f; // Tiempo en segundos antes de apagar la luz

    void Start()
    {
        // Obtén la referencia al componente Light de la farola.
        myLight = GetComponentInChildren<Light>();
        myLight.enabled = false; // Inicia con la luz apagada.
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si es el bus el que entra en el trigger.
        if (other.CompareTag("Bus"))
        {
            StopAllCoroutines(); // Detiene la corutina en caso de que ya estuviera corriendo
            myLight.enabled = true; // Enciende la luz.
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si es el bus el que sale del trigger.
        if (other.CompareTag("Bus"))
        {
            StartCoroutine(TurnOffLightAfterDelay()); // Inicia la corutina para apagar la luz después del retraso
        }
    }

    IEnumerator TurnOffLightAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTurningOff); // Espera el tiempo especificado
        myLight.enabled = false; // Apaga la luz.
    }
}
