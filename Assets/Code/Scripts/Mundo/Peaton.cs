using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaton : MonoBehaviour
{
    

    public string nombrePeaton;
    public TipoPeaton tipoPeaton; // Por ejemplo: "Estudiante", "Profesor", "Visitante", etc.

    private Animator animator;
    private TextMesh textMesh;
    public bool isFocus = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        // Obtener el componente TextMesh en el hijo
        textMesh = GetComponentInChildren<TextMesh>();


        nombrePeaton = GeneradorPeaton.GenerarNombreAleatorio();
        tipoPeaton = GeneradorPeaton.GenerarTipoAleatorio();

        if (textMesh != null)
        {
            // Inicialmente ocultar el TextMesh
            textMesh.gameObject.SetActive(false);
            textMesh.text = $"{nombrePeaton}\n{tipoPeaton}";
        }
        else
        {
            Debug.LogError("No se encontró un componente TextMesh en los hijos del peatón.");
        }

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
    
    public void SetFocus(bool focus)
    {
        isFocus = focus;

        if (textMesh != null)
        {
            textMesh.gameObject.SetActive(isFocus);
        }
    }
}

public enum TipoPeaton
    {
        Estudiante,
        Profesor,
        Visitante,
        Personal,
        Trabajador,
        Turista,
        Niño,
        Anciano
    }

public static class GeneradorPeaton
{
    private static string[] nombres = { "Carlos", "María", "Ana", "Luis", "Juan", "Lucía", "Pedro", "Elena" };

    public static string GenerarNombreAleatorio()
    {
        int indice = UnityEngine.Random.Range(0, nombres.Length);
        return nombres[indice];
    }

    public static TipoPeaton GenerarTipoAleatorio()
    {
        System.Array valores = System.Enum.GetValues(typeof(TipoPeaton));
        TipoPeaton tipoAleatorio = (TipoPeaton)valores.GetValue(UnityEngine.Random.Range(0, valores.Length));
        return tipoAleatorio;
    }
}
