using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timerLimit = 300;
    public float currentTimer;
    public TextMeshProUGUI timerText;
    public bool counting;

    public bool finishLevel;
    public GameObject endingMenu;
    
    public GameObject pauseUI;
    public bool pauseIsOn;
    

    private void Start()
    {
        currentTimer = timerLimit;
        pauseIsOn = false;
    }

    private void Update()
    {
        if (counting)
        {
            currentTimer -= Time.deltaTime;
            timerText.text = "TIME : " + currentTimer.ToString("0");
        }

        if (currentTimer <= 0)
        {
            counting = false;
            currentTimer = 0;
            timerText.text = "TIME : " + currentTimer.ToString("0");

            finishLevel = true;
            

            // Show Ending Menu
            endingMenu.SetActive(true);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseIsOn)
            {
                Time.timeScale = 0;
                
                pauseUI.SetActive(true);
                pauseIsOn = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
                pauseIsOn = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
}
