using UnityEngine;

public class LluviaSeguirCamara : MonoBehaviour
{
    public GameObject rainParticleSystem; // Asigna aquí el sistema de partículas desde el Inspector.
    public Vector3 offset = new Vector3(0, 10, 0); // Offset para que la lluvia aparezca por encima de la cámara.

    void Update()
    {
        if (rainParticleSystem != null)
        {
            // Obtén la posición y la rotación de la cámara
            Vector3 cameraPosition = transform.position;
            Quaternion cameraRotation = transform.rotation;

            // Calcula la nueva posición del sistema de partículas
            Vector3 newPosition = cameraPosition + cameraRotation * offset;

            // Actualiza la posición y rotación del sistema de partículas
            rainParticleSystem.transform.position = newPosition;
            //rainParticleSystem.transform.rotation = cameraRotation;
        }
    }
}
