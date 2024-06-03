using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaton : MonoBehaviour
{
    
    public string nombrePeaton;
    public string tipoPeaton; // Por ejemplo: "Estudiante", "Profesor", "Visitante", etc.

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        // Verificar si el Animator y la animación existen
        if (animator != null)
        {
            // Configurar la animación de caminar para que se reproduzca
            animator.Play("walk");
        }
        else
        {
            Debug.LogError("No se encontró el componente Animator en el peaton.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si es necesario, agregar lógica adicional para la animación o el movimiento del peaton
    }
}
