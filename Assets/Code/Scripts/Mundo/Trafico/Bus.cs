using UnityEngine;

public class Bus : MonoBehaviour
{
    public int cantidadPeatones;

    private Coche _coche;

    public void Start()
    {
        _coche = GetComponent<Coche>();
    }	

    public void IncrementarCantidadPeatones()
    {
        cantidadPeatones++;
        _coche.SetText($"{_coche.matricula} - P:{cantidadPeatones.ToString()}");
    }

    public void DecrementarCantidadPeatones()
    {
        cantidadPeatones--;
        _coche.SetText($"{_coche.matricula} - P:{cantidadPeatones.ToString()}");
    }

    public int getCantidadPeatones()
    {
        return cantidadPeatones;
    }
}
