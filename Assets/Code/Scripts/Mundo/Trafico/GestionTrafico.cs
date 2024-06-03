using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ruta
{
    public List<Transform> wayPoints;
}

public class GestionTrafico : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabsCoches; // Lista de prefabs de coches
    [SerializeField] List<Ruta> rutas; // Lista de rutas
    [SerializeField] int cantidadInicialCoches = 20; // Número inicial de coches a instanciar
    [SerializeField] float intervaloSpawn = 60f; // Intervalo de tiempo para instanciar nuevos coches
    [SerializeField] Transform padreSpawn; // Lista de puntos de spawn para los coches

    private HashSet<Transform> puntosSpawnOcupados = new HashSet<Transform>();

    void Start()
    {
        // Instanciar coches iniciales
        for (int i = 0; i < cantidadInicialCoches; i++)
        {
            InstanciarCocheAleatorio();
        }

        // Iniciar la corrutina para instanciar un coche cada 60 segundos
        StartCoroutine(RutinaSpawnCoches());
    }

    public void InstanciarCocheAleatorio()
    {
        // Seleccionar un prefab aleatorio de la lista
        int indiceAleatorioCoche = Random.Range(0, prefabsCoches.Count);
        GameObject prefabCoche = prefabsCoches[indiceAleatorioCoche];

        // Seleccionar una ruta aleatoria de la lista de rutas
        int indiceAleatorioRuta = Random.Range(0, rutas.Count);
        List<Transform> rutaSeleccionada = rutas[indiceAleatorioRuta].wayPoints;

        if (Random.Range(0f, 1f) <= 0.5f)
        {
            //Debug.Log("Ruta invertida");
            rutaSeleccionada.Reverse();
        }

        // Instanciar el coche en el punto de spawn y establecer el punto de spawn como su padre
        GameObject cocheInstanciado = Instantiate(prefabCoche, padreSpawn.position, padreSpawn.rotation, padreSpawn);

        // Asignar la ruta seleccionada al coche instanciado
        RutasTrafico rutaBus = cocheInstanciado.GetComponent<RutasTrafico>();
        if (rutaBus != null)
        {
            rutaBus.SetWayPoints(rutaSeleccionada);
        }
    }

    IEnumerator RutinaSpawnCoches()
    {
        while (true)
        {
            // Esperar el intervalo de tiempo
            yield return new WaitForSeconds(intervaloSpawn);

            // Instanciar un coche aleatorio
            InstanciarCocheAleatorio();
        }
    }
    
}

