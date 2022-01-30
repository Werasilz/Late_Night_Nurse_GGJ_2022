using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPointController : MonoBehaviour
{
    public GameObject lightPointSwitch;

    public LightPoint[] lightPoints;
    public Image[] buttonImage;
    public Sprite[] buttonClickSprite;

    public AudioClip Switch;
    public AudioSource Asource;

    // Limit Lighting = 3
    private int lightingCount;

    private void Start()
    {
        lightingCount = 0;
    }

    public void TurnOnLightPoint(int index)
    {
        Asource.clip = Switch;
        Asource.Play();
        
        if (!lightPoints[index].lighting)
        {
            // Check limit lighting
            if (lightingCount < 3)
            {
                // Turn on light
                lightingCount += 1;
                lightPoints[index].ShowLighting();
                buttonImage[index].sprite = buttonClickSprite[1];
            }
        }
        else
        {
            // Turn off light
            lightPoints[index].HideLighting();
            lightingCount -= 1;
            buttonImage[index].sprite = buttonClickSprite[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightPointSwitch.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightPointSwitch.SetActive(false);
        }
    }
}
