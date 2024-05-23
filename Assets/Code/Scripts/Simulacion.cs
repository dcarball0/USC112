using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Simulacion : MonoBehaviour
{
    // Variables para simular el clima
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField, Range(-10, 40)] private float temperatura;
    [SerializeField, Range(0, 100)] private float humedad;
    [SerializeField, Range(0, 500)] private int calidadAire; // Índice de calidad del aire (AQI)

    // Propiedades públicas para acceder a los valores desde otros scripts
    public float TimeOfDay => timeOfDay;    

    // Datos históricos o modelos para simular variaciones
    private Dictionary<int, float> temperaturaPorHora;
    private Dictionary<int, int> calidadAirePorHora;

    private void Awake()
    {
        InicializarDatosClima();
    }

    private void Update()
    {
        if (Application.isPlaying)
        {
            SimularDia();
            SimularClima();
        }
    }

    private void InicializarDatosClima()
    {
        // Inicializar datos de temperatura y calidad de aire por hora del día
        temperaturaPorHora = new Dictionary<int, float>
        {
            { 0, 15f }, { 1, 14f }, { 2, 13f }, { 3, 12f }, { 4, 11f }, { 5, 10f },
            { 6, 10f }, { 7, 12f }, { 8, 15f }, { 9, 18f }, { 10, 22f }, { 11, 25f },
            { 12, 27f }, { 13, 28f }, { 14, 29f }, { 15, 28f }, { 16, 27f }, { 17, 25f },
            { 18, 22f }, { 19, 20f }, { 20, 18f }, { 21, 17f }, { 22, 16f }, { 23, 15f }
        };

        calidadAirePorHora = new Dictionary<int, int>
        {
            { 0, 50 }, { 1, 48 }, { 2, 47 }, { 3, 45 }, { 4, 44 }, { 5, 42 },
            { 6, 40 }, { 7, 45 }, { 8, 50 }, { 9, 55 }, { 10, 60 }, { 11, 65 },
            { 12, 70 }, { 13, 75 }, { 14, 80 }, { 15, 85 }, { 16, 90 }, { 17, 85 },
            { 18, 80 }, { 19, 75 }, { 20, 70 }, { 21, 65 }, { 22, 60 }, { 23, 55 }
        };
    }

    private void SimularDia()
    {
        // Simular la hora del día
        timeOfDay += Time.deltaTime * Time.timeScale;
        timeOfDay %= 24; // Asegurar que siempre esté entre 0-24
    }

    private void SimularClima()
    {
        int horaActual = Mathf.FloorToInt(timeOfDay);
        temperatura = temperaturaPorHora[horaActual];
        calidadAire = calidadAirePorHora[horaActual];

        // Simular la humedad basada en la hora del día y la temperatura
        SimularHumedad(horaActual);

        // Log para depuración
        //Debug.Log($"Hora: {horaActual}, Temperatura: {temperatura}°C, Humedad: {humedad}%, Calidad del Aire: {calidadAire} AQI");
    }

    private void SimularHumedad(int horaActual)
    {
        // Modelo simple para variación de la humedad relativa
        float baseHumedad = 70f; // Humedad base durante el día
        float variacionDiurna = Mathf.Sin((horaActual / 24f) * Mathf.PI * 2f) * 20f; // Variación a lo largo del día
        float efectoTemperatura = Mathf.Clamp(30f - temperatura, 0f, 30f); // La humedad relativa disminuye con temperaturas altas

        humedad = baseHumedad + variacionDiurna + efectoTemperatura;
        humedad = Mathf.Clamp(humedad, 0f, 100f); // Asegurarse de que la humedad esté entre 0 y 100
    }

    public float GetTemperatura()
    {
        return temperatura;
    }
    public float GetHumedad()
    {
        return humedad;
    }
    public float GetCalidadAire()
    {
        return calidadAire;
    }   
}
