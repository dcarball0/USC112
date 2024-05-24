using UnityEngine;
using System.Collections.Generic;

public class GestionBasura : MonoBehaviour
{
    public List<Papelera> papeleras;
    public float nivelMedioBasura;

    void Start()
    {
        // Encuentra todas las papeleras en la escena y agrégalas a la lista
        Papelera[] todasLasPapeleras = FindObjectsOfType<Papelera>();
        papeleras = new List<Papelera>(todasLasPapeleras);
    }

    void Update()
    {
        nivelMedioBasura = CalcularNivelMedio();
        //Debug.Log("Nivel medio de basura: " + nivelMedioBasura);
    }

    float CalcularNivelMedio()
    {
        float sumaNiveles = 0f;
        foreach (Papelera papelera in papeleras)
        {
            sumaNiveles += papelera.nivelActual;
        }

        return papeleras.Count > 0 ? sumaNiveles / papeleras.Count : 0f;
    }
}
