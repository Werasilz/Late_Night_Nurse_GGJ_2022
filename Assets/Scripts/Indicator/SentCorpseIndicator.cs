using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SentCorpseIndicator : MonoBehaviour
{
    public GameObject onUI;
    public GameObject onObject;

    public Image indicatorOnUI;
    public Image indicatorOnObject;

    public TextMeshProUGUI incomingTextOnUI;
    public TextMeshProUGUI incomingTextOnObject;

    public void DecreaseIndicator()
    {
        DOVirtual.Float(1f, 0f, 30, SetFillAmount);
    }

    private void SetFillAmount(float value)
    {
        indicatorOnUI.fillAmount = value;
        indicatorOnObject.fillAmount = value;
    }
}
