using UnityEngine;

public class Edificio : MonoBehaviour
{
    // Identificación básica
    public string nombreEdificio;
    public string descripcionEdificio;
    public Vector3 ubicacion; // Coordenadas de ubicación

    // Infraestructura
    public bool tieneRedInalambrica;
    public bool tieneIluminacionInteligente;
    public bool tieneEquiposEmergencia;

    // Energía
    public float consumoElectricidad;
    public float consumoAgua;
    public float consumoCalefaccion;

    // Medio Ambiente
    public float indiceCalidadAire;
    public float nivelRuido;
    public float eficienciaGestionResiduos;

    // Movilidad
    public bool tieneParadaAutobusAutonomo;
    public int cantidadPeatones;
    public int cantidadVehiculos;

    // Seguridad
    public bool tieneCamarasSeguridad;
    public bool tieneControlAcceso;
    public bool tienePlanEvacuacionEmergencia;

    // Métodos para actualizar información (pueden ser más específicos según necesidad)
    public void ActualizarConsumoElectricidad(float nuevoConsumo)
    {
        consumoElectricidad = nuevoConsumo;
    }

    public void ActualizarConsumoAgua(float nuevoConsumo)
    {
        consumoAgua = nuevoConsumo;
    }

    public void ActualizarIndiceCalidadAire(float nuevoIndice)
    {
        indiceCalidadAire = nuevoIndice;
    }

    public void IncrementarCantidadVehiculos()
    {
        cantidadVehiculos++;
    }

    public void IncrementarCantidadPeatones()
    {
        cantidadPeatones++;
    }

    // Otros métodos relevantes según la lógica del juego
}
