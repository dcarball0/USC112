using System;
using UnityEngine;

public class Coche : MonoBehaviour
{
    // Enum para definir los tipos de coches
    public enum TipoCoche
    {
        Particular,
        Autonomo,
        Servicio,
        Emergencia,
        Autobus,
        Electrico,
        Entrega,
        ComunidadUniversitaria
    }

    // Propiedades del coche
    public string matricula;
    public TipoCoche tipo;
    public bool isFocus = false;

    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        matricula = GeneradorMatricula.GenerarMatriculaAleatoria();

        // Encontrar el componente TextMesh en el hijo
        textMesh = GetComponentInChildren<TextMesh>();

        if (textMesh != null)
        {
            // Inicialmente ocultar el TextMesh
            textMesh.gameObject.SetActive(false);
            textMesh.text = $"{matricula} - {tipo.ToString()}"; // Asignar la matrícula y el tipo al TextMesh
        }
        else
        {
            Debug.LogError("No se encontró un componente TextMesh en los hijos del coche.");
        }
    }

    // Método para establecer el enfoque
    public void SetFocus(bool focus)
    {
        isFocus = focus;

        if (textMesh != null)
        {
            textMesh.gameObject.SetActive(isFocus);
        }
    }
}


public class GeneradorMatricula : MonoBehaviour
{
    // Método para generar una matrícula aleatoria
    public static string GenerarMatriculaAleatoria()
    {
        string letras = GenerarLetrasAleatorias(3);
        string numeros = GenerarNumerosAleatorios(4);
        return $"{numeros} {letras}";
    }

    // Método para generar una cadena de letras aleatorias
    private static string GenerarLetrasAleatorias(int longitud)
    {
        const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] resultado = new char[longitud];
        for (int i = 0; i < longitud; i++)
        {
            resultado[i] = letras[UnityEngine.Random.Range(0, letras.Length)];
        }
        return new string(resultado);
    }

    // Método para generar una cadena de números aleatorios
    private static string GenerarNumerosAleatorios(int longitud)
    {
        char[] resultado = new char[longitud];
        for (int i = 0; i < longitud; i++)
        {
            resultado[i] = UnityEngine.Random.Range(0, 10).ToString()[0];
        }
        return new string(resultado);
    }
}
