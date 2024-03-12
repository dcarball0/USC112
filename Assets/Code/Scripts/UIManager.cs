using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            //Debug.LogError("UIDocument component not found on the GameObject.");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;
        ProgressBar dayBar = root.Q<ProgressBar>("DayProgressBar");
        if (dayBar == null)
        {
           // Debug.LogError("ProgressBar with name 'DayProgressBar' not found.");
            return;
        }

        dayBar.lowValue = 0;
        dayBar.highValue = 24; // Aquí parece haber un error tipográfico. Debe ser highValue, no lowValue nuevamente.
    }



}
