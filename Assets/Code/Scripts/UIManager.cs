using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject sceneManager;
    private CicloDiaNoche lightingManager;
    
    ProgressBar dayBar;
    Button timeScaleButton;
    private void OnEnable()
    {
        lightingManager = sceneManager.GetComponent<CicloDiaNoche>();

        UIDocument uiDocument = GetComponent<UIDocument>();

        VisualElement root = uiDocument.rootVisualElement;
        dayBar = root.Q<ProgressBar>();
        timeScaleButton = root.Q<Button>(name: "TimeScaleButton");
        timeScaleButton.RegisterCallback<ClickEvent>(lightingManager.ChangeTimeScale);
        

        if (dayBar == null)
        {
            Debug.LogError("ProgressBar with name 'DayProgressBar' not found.");
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
