using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ProgressBar dayBar = root.Q<ProgressBar>("DayProgressBar");
        dayBar.lowValue = 0;
        dayBar.lowValue = 24;

        
    }
    

}
