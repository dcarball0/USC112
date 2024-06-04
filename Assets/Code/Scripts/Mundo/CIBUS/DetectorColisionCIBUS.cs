using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorColisionCIBUS : MonoBehaviour
{
    private bool esEntrada = false;

    private GestionTraficoCIBUS _gestionTraficoCIBUS;

    // Start is called before the first frame update
    void Start()
    {
        _gestionTraficoCIBUS = GetComponentInParent<GestionTraficoCIBUS>();

        if (gameObject.name.Contains("Entrada"))
            esEntrada = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(esEntrada)
        {
            if (other.GetComponent<Coche>())
                _gestionTraficoCIBUS.IncrementarEntradaCoche();
            else if (other.GetComponent<Peaton>() && esEntrada)
                _gestionTraficoCIBUS.IncrementarEntradaPeaton();
        }
        else
        {
            if (other.GetComponent<Coche>())
                _gestionTraficoCIBUS.IncrementarSalidaCoche();
            else if (other.GetComponent<Peaton>() && !esEntrada)
                _gestionTraficoCIBUS.IncrementarSalidaPeaton();
        }
    }
}
