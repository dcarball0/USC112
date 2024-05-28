using UnityEngine;

public class Papelera : MonoBehaviour
{
    private float capacidadMaxima = 100f;
   [SerializeField, Range(0, 100)] public float nivelActual = 0f;
    public GameObject iconoLleno;

    void Start()
    {
        iconoLleno.SetActive(false);
    }

    void Update()
    {      
        if (nivelActual >= capacidadMaxima)
        {
            MostrarIconoLleno();
        }
        else
            nivelActual += Time.deltaTime; // Ejemplo simple: llenar con el tiempo
    }

    void MostrarIconoLleno()
    {
        iconoLleno.SetActive(true);
    }

    public void VaciarPapelera()
    {
        nivelActual = 0f;
        iconoLleno.SetActive(false);
    }
}
