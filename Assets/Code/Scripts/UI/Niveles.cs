using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Niveles : MonoBehaviour
{
    public GameObject contaminacion;
    // Start is called before the first frame update
    void Start()
    {
        contaminacion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleContaminacion(ClickEvent evt)
    {
        // Alternar la visibilidad del objeto
        // TODO: Hacer vista cenital
        contaminacion.SetActive(!contaminacion.activeSelf);
    }
}
