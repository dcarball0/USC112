using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionTraficoCIBUS : MonoBehaviour
{
    [Header("Entrada")]
    public GameObject entradaTextObject;
    [SerializeField]private int entradaPeatonCount = 0;
    [SerializeField]private int entradaCocheCount = 0;

    [Header("Salida")]
    public GameObject salidaTextObject;
    [SerializeField]private int salidaPeatonCount = 0;
    [SerializeField]private int salidaCocheCount = 0;


    private TextMesh entradaTextMesh;
    private TextMesh salidaTextMesh;

    void Start()
    {
        // Inicializar los TextMesh
        entradaTextMesh = entradaTextObject.GetComponent<TextMesh>();
        salidaTextMesh = salidaTextObject.GetComponent<TextMesh>();

        // Comprobar si los TextMesh se han encontrado correctamente
        if (entradaTextMesh == null)
        {
            Debug.LogError("No se encontró un componente TextMesh en el objeto de texto de entrada.");
        }
        if (salidaTextMesh == null)
        {
            Debug.LogError("No se encontró un componente TextMesh en el objeto de texto de salida.");
        }

        UpdateText();
    }

    void UpdateText()
    {
        if (entradaTextMesh != null)
            entradaTextMesh.text = $"Entrada:\nPeatones: {entradaPeatonCount}\nCoches: {entradaCocheCount}";

        if (salidaTextMesh != null)
            salidaTextMesh.text = $"Salida:\nPeatones: {salidaPeatonCount}\nCoches: {salidaCocheCount}";
    }

    public void IncrementarEntradaPeaton()
    {
        entradaPeatonCount++;
        UpdateText();
    }

    public void IncrementarEntradaCoche()
    {
        entradaCocheCount++;
        UpdateText();
    }

    public void IncrementarSalidaPeaton()
    {
        salidaPeatonCount++;
        UpdateText();
    }

    public void IncrementarSalidaCoche()
    {
        salidaCocheCount++;
        UpdateText();
    }

    
}
