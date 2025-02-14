using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    private Button button;

    private GameManager gameManagerScript;

    private TextMeshProUGUI buttonText;

    private GameObject gameModes;

    public GameObject difficulties;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameModes = GameObject.Find("Game Modes");
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectMode);
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectMode()
    {
        Debug.Log(gameObject.name + " was clicked");
        Debug.Log(buttonText.text);

        if (buttonText.text == "Classic")
        {
            ClearGameModeButtons();
        }
        else
        {
            Debug.Log("powered up clicked");
            gameManagerScript.StartPoweredUpGame();
        }
        //gameManagerScript.StartPoweredUpGame();
    }

    void ClearGameModeButtons()
    {
        gameModes.SetActive(false);
        difficulties.SetActive(true);
    }
}
