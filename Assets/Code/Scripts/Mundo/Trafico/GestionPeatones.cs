using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPeatones : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabsPeatones; // Lista de prefabs de peatones
    [SerializeField] List<Ruta> rutas; // Lista de rutas
    [SerializeField] int cantidadInicialPeatones = 100; // Número inicial de peatones a instanciar
    [SerializeField] float intervaloSpawn = 10f; // Intervalo de tiempo para instanciar nuevos peatones
    [SerializeField] Transform padreSpawn; // Transform padre para los peatones

    public int numeroPeatonesActual = 0;

    void Start()
    {
        // Instanciar peatones iniciales
        for (int i = 0; i < cantidadInicialPeatones; i++)
        {
            InstanciarPeatonAleatorio();
        }

        // Iniciar la corrutina para instanciar un peatón cada 60 segundos
        StartCoroutine(RutinaSpawnPeatones());
    }

    public void InstanciarPeatonAleatorio()
    {
        // Seleccionar un prefab aleatorio de la lista
        int indiceAleatorioPeaton = Random.Range(0, prefabsPeatones.Count);
        GameObject prefabPeaton = prefabsPeatones[indiceAleatorioPeaton];

        // Seleccionar una ruta aleatoria de la lista de rutas
        int indiceAleatorioRuta = Random.Range(0, rutas.Count);
        List<Transform> rutaSeleccionada = rutas[indiceAleatorioRuta].wayPoints;

        if (Random.Range(0f, 1f) <= 0.5f)
        {
            //Debug.Log("Ruta invertida");
            rutaSeleccionada.Reverse();
        }

        // Instanciar el peatón en el punto de spawn y establecer el punto de spawn como su padre
        GameObject peatonInstanciado = Instantiate(prefabPeaton, padreSpawn.position, padreSpawn.rotation, padreSpawn);

        // Asignar la ruta seleccionada al peatón instanciado
        RutasTrafico rutaPeaton = peatonInstanciado.GetComponent<RutasTrafico>();
        if (rutaPeaton != null)
        {
            rutaPeaton.SetWayPoints(rutaSeleccionada);
        }

        numeroPeatonesActual++;
    }

    IEnumerator RutinaSpawnPeatones()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo
            yield return new WaitForSeconds(intervaloSpawn);

            // Instanciar un peatón aleatorio
            InstanciarPeatonAleatorio();
        }
    }

    public int GetNumeroPeatones()
    {
        return numeroPeatonesActual;
    }
}
