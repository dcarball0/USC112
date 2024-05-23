using UnityEngine;

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

            // Ajustar el tamaño del BoxCollider al tamaño del MeshRenderer
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                collider.center = meshRenderer.bounds.center - child.transform.position;
                collider.size = meshRenderer.bounds.size;
            }
        }

    }
}
