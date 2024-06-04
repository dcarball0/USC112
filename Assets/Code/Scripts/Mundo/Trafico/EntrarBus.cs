using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarBus : MonoBehaviour
{
    Bus _bus;
    [SerializeField] GameObject sceneManager;
    GestionPeatones _gestionPeatones;
    [SerializeField] [Range(0, 100)] float probabilidadEntrar = 80f; // Porcentaje de probabilidad de entrar al bus

    [SerializeField] bool hayBus = false;
    
    void Start()
    {
        _gestionPeatones = sceneManager.GetComponent<GestionPeatones>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Colisión con " + other.name);
        if (other.GetComponent<Bus>())
        {
            hayBus = true;
            _bus = other.GetComponent<Bus>();
            other.GetComponent<Coche>().SetTextActive(true);
        }
        if (other.GetComponent<Peaton>() && _bus != null)
        {
            float probabilidad = Random.Range(0f, 100f);
            if (probabilidad <= probabilidadEntrar) // Comparar con el porcentaje
            {
                _bus.IncrementarCantidadPeatones();
                Destroy(other.gameObject); // Eliminar el coche
                _gestionPeatones.InstanciarPeatonAleatorio(); // Instanciar un nuevo coche en un lugar aleatorio
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Bus>())
        {
            hayBus = false;
            other.GetComponent<Coche>().SetTextActive(false);
            _bus = null;
        }
    }
}
