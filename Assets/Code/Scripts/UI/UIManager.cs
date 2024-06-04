using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneManager;
    private CicloDiaNoche lightingManager;
    private Niveles niveles;
    private GestionTrafico gestionTrafico;
    private GestionPeatones gestionPeatones;
    private GestionTorresConexion gestionTorresConexion;

    ProgressBar dayBar;
    Button timeScaleButton, contaminacionAmbientalButton, contaminacionAcusticaButton, consumoAguaButton, consumoElectricidadButton, conexionButton;
    Label temperaturaLabel, humedadLabel, calidadAireLabel, gestionBasuraLabel, cochesLabel, peatonesLabel;

    // Instancia Singleton para acceso fácil
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Referencias a scripts
        lightingManager = sceneManager.GetComponent<CicloDiaNoche>();
        niveles = sceneManager.GetComponent<Niveles>();
        gestionTrafico = sceneManager.GetComponent<GestionTrafico>();
        gestionPeatones = sceneManager.GetComponent<GestionPeatones>();
        gestionTorresConexion = sceneManager.GetComponent<GestionTorresConexion>();

        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;

        // Callbacks
        dayBar = root.Q<ProgressBar>();
        timeScaleButton = root.Q<Button>(name: "TimeScaleButton");
        timeScaleButton.RegisterCallback<ClickEvent>(lightingManager.ChangeTimeScale);

        contaminacionAmbientalButton = root.Q<Button>(name: "ContaminacionAmbientalButton");
        contaminacionAmbientalButton.RegisterCallback<ClickEvent>(niveles.ToggleContaminacionAmbiental);

        contaminacionAcusticaButton = root.Q<Button>(name: "ContaminacionAcusticaButton");
        contaminacionAcusticaButton.RegisterCallback<ClickEvent>(niveles.ToggleContaminacionAcustica);

        consumoAguaButton = root.Q<Button>(name: "ConsumoAguaButton");
        consumoAguaButton.RegisterCallback<ClickEvent>(niveles.ToggleConsumoAgua);

        consumoElectricidadButton = root.Q<Button>(name: "ConsumoElectricidadButton");
        consumoElectricidadButton.RegisterCallback<ClickEvent>(niveles.ToggleConsumoElectrico);

        conexionButton = root.Q<Button>(name: "ConexionButton");
        conexionButton.RegisterCallback<ClickEvent>(gestionTorresConexion.ToggleConexion);

        temperaturaLabel = root.Q<Label>(name: "TemperaturaLabel");
        humedadLabel = root.Q<Label>(name: "HumedadLabel");
        calidadAireLabel = root.Q<Label>(name: "CalidadAireLabel");
        gestionBasuraLabel = root.Q<Label>(name: "GestionBasuraLabel");
        cochesLabel = root.Q<Label>(name: "CochesLabel");
        peatonesLabel = root.Q<Label>(name: "PeatonesLabel");

        if (dayBar == null)
        {
            Debug.LogError("ProgressBar with name 'DayProgressBar' not found.");
            return;
        }

        dayBar.lowValue = 0;
        dayBar.highValue = 24;
    }

    private void LateUpdate()
    {
        dayBar.value = lightingManager.GetTimeOfDay();
        dayBar.title = $"{sceneManager.GetComponent<Simulacion>().GetDate():dd/MM/yyyy}";
        temperaturaLabel.text = $"{sceneManager.GetComponent<Simulacion>().GetTemperatura()}°C";
        humedadLabel.text = $"{sceneManager.GetComponent<Simulacion>().GetHumedad():F1}%";
        calidadAireLabel.text = $"{sceneManager.GetComponent<Simulacion>().GetCalidadAire()} AQI";
        gestionBasuraLabel.text = $"{sceneManager.GetComponent<Simulacion>().GetGestionBasura():F1}%";
        cochesLabel.text = $"C: {gestionTrafico.GetNumeroCoches()}";
        peatonesLabel.text = $"P: {gestionPeatones.GetNumeroPeatones()}";
    }
}
