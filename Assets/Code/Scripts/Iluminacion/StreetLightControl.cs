using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para usar corutinas

public class StreetLightControl : MonoBehaviour
{
    private Light myLight;
    public float delayBeforeTurningOff = 0.5f; // Tiempo en segundos antes de apagar la luz
    private int objectsInRange = 0; // Contador de objetos dentro del rango
    public GameObject sceneManager;
    private Simulacion simulacion;

    void Start()
    {
        // Obtén la referencia al componente Light de la farola.
        myLight = GetComponentInChildren<Light>();
        myLight.enabled = false; // Inicia con la luz apagada.

        simulacion = sceneManager.GetComponent<Simulacion>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(simulacion.isNight)
        {
            StopAllCoroutines(); // Detiene la corutina en caso de que ya estuviera corriendo
            myLight.enabled = false; // Enciende la luz.
            return;
        }
        // Verifica si es un coche o un peatón el que entra en el trigger.
        if (other.CompareTag("Bus") || other.GetComponent<Coche>() != null || other.GetComponent<Peaton>() != null)
        {
            objectsInRange++; // Incrementa el contador de objetos en el rango
            StopAllCoroutines(); // Detiene la corutina en caso de que ya estuviera corriendo
            myLight.enabled = true; // Enciende la luz.
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si es un coche o un peatón el que sale del trigger.
        if (other.CompareTag("Bus") || other.GetComponent<Coche>() != null || other.GetComponent<Peaton>() != null)
        {
            objectsInRange--; // Decrementa el contador de objetos en el rango

            // Solo apaga la luz si no hay más objetos en el rango
            if (objectsInRange <= 0)
            {
                StartCoroutine(TurnOffLightAfterDelay()); // Inicia la corutina para apagar la luz después del retraso
            }
        }
    }

    IEnumerator TurnOffLightAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTurningOff); // Espera el tiempo especificado
        myLight.enabled = false; // Apaga la luz.
    }
}
