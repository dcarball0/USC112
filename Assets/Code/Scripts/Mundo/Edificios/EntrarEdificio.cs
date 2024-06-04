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
    [SerializeField] Transform entryWaypoint; // Waypoint al que deben moverse los objetos

    void Start()
    {
        _edificio = edificio.GetComponent<Edificio>();
        _gestionTrafico = sceneManager.GetComponent<GestionTrafico>();
        _gestionPeatones = sceneManager.GetComponent<GestionPeatones>();

        if (entryWaypoint == null)
        {
            Debug.LogError("Waypoint no asignado en el objeto EntrarEdificio.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coche>() && !other.GetComponent<Bus>())
        {
            float probabilidad = Random.Range(0f, 100f);
            if (probabilidad <= probabilidadCoche) // Comparar con el porcentaje
            {
                _edificio.IncrementarCantidadVehiculos();
                RutasTrafico rutasTrafico = other.GetComponent<RutasTrafico>();
                if (rutasTrafico != null)
                {
                    rutasTrafico.EnterBuilding(entryWaypoint);
                }
                _gestionTrafico.InstanciarCocheAleatorio(); // Instanciar un nuevo coche en un lugar aleatorio
            }
        }

        if (other.GetComponent<Peaton>())
        {
            float probabilidad = Random.Range(0f, 100f);
            if (probabilidad <= probabilidadCoche) // Comparar con el porcentaje
            {
                _edificio.IncrementarCantidadPeatones();
                RutasTrafico rutasTrafico = other.GetComponent<RutasTrafico>();
                if (rutasTrafico != null)
                {
                    rutasTrafico.EnterBuilding(entryWaypoint);
                }
                _gestionPeatones.InstanciarPeatonAleatorio(); // Instanciar un nuevo coche en un lugar aleatorio
            }
        }
    }
}
