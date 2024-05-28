using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Niveles : MonoBehaviour
{
    public GameObject contaminacionAmbiental;
    public GameObject contaminacionAcustica;
    public GameObject consumoAgua;
    public GameObject consumoElectricidad;

    // Start is called before the first frame update
    void Start()
    {
        contaminacionAmbiental.SetActive(false);
        contaminacionAcustica.SetActive(false);
        consumoAgua.SetActive(false);
        consumoElectricidad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleContaminacionAmbiental(ClickEvent evt)
    {
        // Alternar la visibilidad del objeto
        // TODO: Hacer vista cenital
        contaminacionAmbiental.SetActive(!contaminacionAmbiental.activeSelf);
        contaminacionAcustica.SetActive(false);
        consumoAgua.SetActive(false);
        consumoElectricidad.SetActive(false);
    }
    
    public void ToggleContaminacionAcustica(ClickEvent evt)
    {
        // Alternar la visibilidad del objeto
        // TODO: Hacer vista cenital
        contaminacionAcustica.SetActive(!contaminacionAcustica.activeSelf);
        contaminacionAmbiental.SetActive(false);
        consumoAgua.SetActive(false);
        consumoElectricidad.SetActive(false);
    }
    
    public void ToggleConsumoAgua(ClickEvent evt)
    {
        // Alternar la visibilidad del objeto
        // TODO: Hacer vista cenital
        consumoAgua.SetActive(!consumoAgua.activeSelf);
        contaminacionAmbiental.SetActive(false);
        contaminacionAcustica.SetActive(false);
        consumoElectricidad.SetActive(false);
    }
    
    public void ToggleConsumoElectrico(ClickEvent evt)
    {
        // Alternar la visibilidad del objeto
        // TODO: Hacer vista cenital
        consumoElectricidad.SetActive(!consumoElectricidad.activeSelf);
        contaminacionAmbiental.SetActive(false);
        contaminacionAcustica.SetActive(false);
        consumoAgua.SetActive(false);
    }
}
