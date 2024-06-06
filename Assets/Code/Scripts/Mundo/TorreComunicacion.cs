using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreComunicacion : MonoBehaviour
{
    [SerializeField] private SphereCollider sphereCollider; // Collider de la torre
    [SerializeField] private TextMesh textMesh; // TextMesh para mostrar el número de peatones conectados
    [SerializeField] private ParticleSystem particleSystem; // Partículas de la torre
    [SerializeField] private LayerMask layerMask; // Máscara de capa para filtrar los peatones

    [SerializeField] private bool isTextVisible = false; // Visibilidad del texto
    [SerializeField] private bool isParticleVisible = false; // Visibilidad de las partículas

    void Start()
    {
        if (sphereCollider == null)
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<TextMesh>();
        }
        if (particleSystem == null)
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        if (sphereCollider == null || textMesh == null)
        {
            Debug.LogError("Falta asignar SphereCollider o TextMesh en la Torre de Comunicación.");
            enabled = false;
        }
        else
        {
            textMesh.gameObject.SetActive(isTextVisible);
            particleSystem.Stop();
        }
    }

    void Update()
    {
        if (isTextVisible)
        {
            int numberOfPeatones = GetNumberOfPeatonesInside();
            UpdateTextMesh(numberOfPeatones);
        }
    }

    int GetNumberOfPeatonesInside()
    {
        // Obtener la posición y el radio de la esfera
        Vector3 sphereCenter = sphereCollider.bounds.center;
        float sphereRadius = sphereCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z); // Considerar el escalado del objeto

        // Obtener todos los colliders dentro de la esfera
        Collider[] collidersInside = Physics.OverlapSphere(sphereCenter, sphereRadius, layerMask);

        // Contar los peatones
        int count = 0;
        foreach (Collider col in collidersInside)
        {
            if (col.GetComponent<Peaton>() != null)
            {
                count++;
            }
        }

        return count;
    }

    void UpdateTextMesh(int numberOfPeatones)
    {
        textMesh.text = "Conectados: " + numberOfPeatones;
    }

    public void ToggleTextVisibility()
    {
        isTextVisible = !isTextVisible;
        textMesh.gameObject.SetActive(isTextVisible);
    }

    public void ToggleParticleVisibility()
    {
        if (particleSystem != null)
        {
            isParticleVisible = particleSystem.isPlaying;
            if (isParticleVisible)
            {
                particleSystem.Stop();
            }
            else
            {
                particleSystem.Play();
            }
        }
    }
}