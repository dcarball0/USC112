using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarEdificio : MonoBehaviour
{
    [SerializeField] GameObject edificio;
    Edificio _edificio;
    [SerializeField] GameObject sceneManager;
    GestionTrafico _gestionTrafico;
    GestionPeatones _gestionPeatones;
    [SerializeField] [Range(0, 100)] float probabilidadCoche = 10f; // Porcentaje de probabilidad de entrar al edificio
    
    void Start()
    {
        _edificio = edificio.GetComponent<Edificio>();
        _gestionTrafico = sceneManager.GetComponent<GestionTrafico>();
        _gestionPeatones = sceneManager.GetComponent<GestionPeatones>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Colisión con " + other.name);
        // Si el objeto que colisiona es un coche y no es un bus
        if (other.GetComponent<Coche>())
        {
            float probabilidad = Random.Range(0f, 100f);
            if (probabilidad <= probabilidadCoche) // Comparar con el porcentaje
            {
                _edificio.IncrementarCantidadVehiculos();
                Destroy(other.gameObject); // Eliminar el coche
                _gestionTrafico.InstanciarCocheAleatorio(); // Instanciar un nuevo coche en un lugar aleatorio
            }
        }

        if (other.GetComponent<Peaton>())
        {
            float probabilidad = Random.Range(0f, 100f);
            if (probabilidad <= probabilidadCoche) // Comparar con el porcentaje
            {
                _edificio.IncrementarCantidadPeatones();
                Destroy(other.gameObject); // Eliminar el coche
                _gestionTrafico.InstanciarCocheAleatorio(); // Instanciar un nuevo coche en un lugar aleatorio
            }
        }
    }
}
