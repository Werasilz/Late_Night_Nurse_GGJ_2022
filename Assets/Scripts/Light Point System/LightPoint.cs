using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPoint : MonoBehaviour
{
    public GameObject lightPoint;
    public bool lighting;

    private void Start()
    {
        lightPoint.SetActive(false);
    }

    public void ShowLighting()
    {
        lighting = true;
        lightPoint.SetActive(true);
    }

    public void HideLighting()
    {
        lighting = false;
        lightPoint.SetActive(false);
    }
}
