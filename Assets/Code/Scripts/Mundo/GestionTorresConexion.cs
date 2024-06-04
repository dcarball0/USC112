using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GestionTorresConexion : MonoBehaviour
{
    [SerializeField] private List<TorreComunicacion> torresDeComunicacion; // Lista de todas las torres de comunicación

    void Start()
    {
        // Obtener todas las torres de comunicación que son hijos del objeto actual
            torresDeComunicacion = new List<TorreComunicacion>(GetComponentsInChildren<TorreComunicacion>());

            if (torresDeComunicacion.Count == 0)
            {
                Debug.LogError("No se encontraron torres de comunicación como hijos de " + gameObject.name);
            }    
    }

    void Update()
    {
        // Lógica de actualización si es necesario
    }

    public void ToggleConexion(ClickEvent evt)
    {
        // Alternar la visibilidad del texto de todas las torres de comunicación
        foreach (TorreComunicacion torre in torresDeComunicacion)
        {
            torre.ToggleTextVisibility();
            torre.ToggleParticleVisibility();
        }
    }
}
