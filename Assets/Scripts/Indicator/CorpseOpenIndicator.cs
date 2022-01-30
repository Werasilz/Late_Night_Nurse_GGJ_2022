using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CorpseOpenIndicator : MonoBehaviour
{
    public GameObject onObject;

    public Image indicatorOnObject;

    public TextMeshProUGUI incomingTextOnObject;

    public void DecreaseIndicator(Image image)
    {
        indicatorOnObject = image;
        DOVirtual.Float(1f, 0f, 20, SetFillAmount);
    }

    private void SetFillAmount(float value)
    {
        indicatorOnObject.fillAmount = value;
    }
}
