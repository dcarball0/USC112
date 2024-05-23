using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneManager;
    private CicloDiaNoche lightingManager;
    private Niveles niveles;
    
    ProgressBar dayBar;
    Button timeScaleButton, contaminacionButton;
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

        contaminacionButton = root.Q<Button>(name: "ContaminacionButton");
        contaminacionButton.RegisterCallback<ClickEvent>(niveles.ToggleContaminacion);
        

        if (dayBar == null)
        {
            UnityEngine.Debug.LogError("ProgressBar with name 'DayProgressBar' not found.");
            return;
        }

        // root.Q(className: "unity-progress-bar__progress");

        dayBar.lowValue = 0;
        dayBar.highValue = 24; 
    }

    private void LateUpdate () {
        dayBar.value = lightingManager.TimeOfDay;
    }



}
