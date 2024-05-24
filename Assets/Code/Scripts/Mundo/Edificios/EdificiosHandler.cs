using UnityEngine;
using System.Collections;

public class BuildingHandler : MonoBehaviour
{
    void Start()
    {
        // Recorrer todos los hijos del objeto padre
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Tag") || child.name.Contains("097") || child.name.Contains("Curtidos"))
            {
                continue;
            }
            // Agregar un BoxCollider si no existe
            BoxCollider collider = child.gameObject.GetComponent<BoxCollider>();
            if (collider == null)
            {
                collider = child.gameObject.AddComponent<BoxCollider>();
            }

            // Agregar Edificio si no existe
            Edificio edificio = child.gameObject.GetComponent<Edificio>();
            if (edificio == null) // Corrección: usar '==' en lugar de '='
            {
                edificio = child.gameObject.AddComponent<Edificio>();
                edificio.nombreEdificio = child.name.Replace("Areas:building.Name:", "");
            }
            else
            {
                edificio.nombreEdificio = child.name.Replace("Areas:building.Name:", "");
            }

            // Ajustar el tamaño del BoxCollider al tamaño del MeshRenderer
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                collider.center = meshRenderer.bounds.center - child.transform.position;
                collider.size = meshRenderer.bounds.size;
            }
        }

        // Iniciar la corrutina para actualizar la información de los edificios
        StartCoroutine(ActualizarInformacionEdificios());
    }

    private IEnumerator ActualizarInformacionEdificios()
    {
        while (true)
        {
            // Actualizar información de los edificios
            foreach (Transform child in transform)
            {
                Edificio edificio = child.gameObject.GetComponent<Edificio>();
                if (edificio != null)
                {
                    // Actualizar consumo de electricidad
                    edificio.ActualizarConsumoElectricidad(Random.Range(0.0f, 100.0f));
                    // Actualizar consumo de agua
                    edificio.ActualizarConsumoAgua(Random.Range(0.0f, 100.0f));
                    // Actualizar índice de calidad de aire
                    edificio.ActualizarIndiceCalidadAire(Random.Range(0.0f, 100.0f));
                }
            }

            // Esperar 1 segundo antes de la próxima actualización
            yield return new WaitForSeconds(1.0f);
        }
    }
}
