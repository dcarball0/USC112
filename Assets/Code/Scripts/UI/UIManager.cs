using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneManager;
    private CicloDiaNoche lightingManager;
    private Niveles niveles;

    ProgressBar dayBar;
    Button timeScaleButton, contaminacionAmbientalButton, contaminacionAcusticaButton, consumoAguaButton, consumoElectricidadButton;

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
        dayBar.value = lightingManager.TimeOfDay;
    }
}
