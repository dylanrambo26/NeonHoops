using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ColorChanging : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    private float colorValue;
    // Start is called before the first frame update
    void Start()
    {
        colorValue = 0;
        ChangeColor();
        //InvokeRepeating("ChangeColor", 1, 50);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void ChangeColor()
    {
        while (colorValue < 1.0f)
        {
            textObject.color = new Color(colorValue, colorValue, colorValue, 1);
            colorValue += 0.001f;
            Debug.Log(colorValue);
        }
    }
}
