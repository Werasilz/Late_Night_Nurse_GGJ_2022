using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    PlayerStatus playerStatus;
    PlayerController playerController;
    ExitWay exitWay;

    private void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerController = FindObjectOfType<PlayerController>();
        exitWay = FindObjectOfType<ExitWay>();
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (exitWay.hasOrderCorpse)
        {
            if (exitWay.orderCorpseID == playerStatus.currentCorpseID)
            {
                Debug.Log("Sent Corpse Exit");
                playerStatus.GetScore(30);
                playerStatus.GetHP();
                SoundEffectManager.Instance.PlayAudio(SoundEffectManager.Instance.AudioClips[0].exitDoor, this.transform.position, 0.5f);

                // Change animation to normal
                playerController.ChangeAnimationLayer(false);

                // Set Corpse to player
                playerStatus.isCarryCorpse = false;
                playerStatus.currentCorpseID = 0;

                // Reset variable
                exitWay.hasOrderCorpse = false;
                exitWay.orderCorpseID = 0;
                exitWay.startIndicator = false;
                exitWay.orderTimer = 0;
            }
        }
    }

    public void ShowInteractPopup()
    {

    }

    public void HideInteractPopup()
    {

    }
}
