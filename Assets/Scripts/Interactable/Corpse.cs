using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour, IInteractable
{
    EntranceWay entranceWay;
    PlayerStatus playerStatus;
    PlayerController playerController;

    public int corpseID;

    private void Awake()
    {
        entranceWay = FindObjectOfType<EntranceWay>();
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        Invoke(nameof(DestroyCorpse), 15);
    }

    private void DestroyCorpse()
    {
        playerStatus.DecreaseHealth();
        entranceWay.hasCorpse = false;
        entranceWay.startIndicator = false;
        Destroy(gameObject);
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (playerStatus.isCarryCorpse == false)
        {
            Debug.Log("Collect Corpse");
            playerStatus.GetScore(10);
            SoundEffectManager.Instance.PlayBagSound(this.transform);

            // Change to carry corpse animation
            playerController.ChangeAnimationLayer(true);

            // Set Corpse to player
            playerStatus.isCarryCorpse = true;
            playerStatus.currentCorpseID = corpseID;

            // Reset variable
            entranceWay.hasCorpse = false;
            entranceWay.startIndicator = false;

            Destroy(gameObject);
        }
    }

    public void ShowInteractPopup()
    {

    }

    public void HideInteractPopup()
    {

    }
}
