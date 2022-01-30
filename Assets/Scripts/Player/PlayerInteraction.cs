using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public IInteractable interactable;
    public GameObject interactPopup;
    public GameObject wrongPopup;
    public GameObject needLightPopup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactable = other.GetComponent<IInteractable>();

            if (interactable == null) return;

            interactable.ShowInteractPopup();
            interactPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (interactable == null) return;

            interactable.HideInteractPopup();
            interactable = null;
            interactPopup.SetActive(false);
        }
    }

    public void ShowWrongPopup()
    {
        wrongPopup.SetActive(true);
        Invoke(nameof(HideWrongPopup), 0.5f);
    }

    private void HideWrongPopup()
    {
        wrongPopup.SetActive(false);
    }

    public void ShowNeedLightPopup()
    {
        needLightPopup.SetActive(true);
        Invoke(nameof(HideNeedLightPopup), 0.5f);
    }

    private void HideNeedLightPopup()
    {
        needLightPopup.SetActive(false);
    }
}
