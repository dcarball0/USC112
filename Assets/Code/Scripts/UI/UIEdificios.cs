using UnityEngine;
using UnityEngine.UIElements;

public class UIEdificios : MonoBehaviour
{
    private Label edificioNameLabel;
    private Label edificioInfoLabel;
    private VisualElement edificioInfoPanel;

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;
        
        edificioInfoPanel = root.Q<VisualElement>(name: "EdificioInfoPanel");
        edificioNameLabel = root.Q<Label>(name: "EdificioNameLabel");
        edificioInfoLabel = root.Q<Label>(name: "EdificioInfoLabel");

        // Referencias a elementos dentro del popup
        Button closeButton = root.Q<Button>("CloseButton");
        closeButton.clicked += () => edificioInfoPanel.style.display = DisplayStyle.None;

        edificioInfoPanel.style.display = DisplayStyle.None;
    }

    public void MostrarInformacionEdificio(Edificio edificio)
    {
        edificioNameLabel.text = edificio.nombreEdificio;
        //edificioDescriptionLabel.text = edificio.descripcionEdificio;
        
        string detalles = $"Consumo de Electricidad: {edificio.consumoElectricidad} kWh\n" +
                          $"Consumo de Agua: {edificio.consumoAgua} litros\n" +
                          $"Índice de Calidad del Aire: {edificio.indiceCalidadAire}\n" +
                          $"Cantidad de Peatones: {edificio.cantidadPeatones}\n" +
                          $"Cantidad de Vehículos: {edificio.cantidadVehiculos}\n" +
                          $"Tiene Iluminación Inteligente: {edificio.tieneIluminacionInteligente}\n" +
                          $"Tiene Cámaras de Seguridad: {edificio.tieneCamarasSeguridad}\n" +
                          $"Tiene Equipos de Emergencia: {edificio.tieneEquiposEmergencia}";

        edificioInfoLabel.text = detalles;

        edificioInfoPanel.style.display = DisplayStyle.Flex;
    }

    public void OcultarInformacionEdificio()
    {
        edificioInfoPanel.style.display = DisplayStyle.None;
    }
}
