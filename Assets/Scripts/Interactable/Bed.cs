using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    PlayerStatus playerStatus;
    PlayerController playerController;
    EntranceWay entranceWay;
    ExitWay exitWay;
    CorpseOnBed corpseOnBed;
    LightPoint lightPoint;

    public int bedID;
    public bool canUseBed = true;

    private void Awake()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerController = FindObjectOfType<PlayerController>();
        entranceWay = FindObjectOfType<EntranceWay>();
        exitWay = FindObjectOfType<ExitWay>();
        corpseOnBed = GetComponentInChildren<CorpseOnBed>();
        lightPoint = GetComponentInParent<LightPoint>();
    }

    public void Interact(PlayerInteraction playerInteraction)
    {
        if (canUseBed == false) return;

        if (lightPoint.lighting)
        {
            if (playerStatus.isCarryCorpse)
            {
                // Check bed with corpse id
                if (bedID == playerStatus.currentCorpseID)
                {
                    Debug.Log("Correct Bed!");
                    playerStatus.GetScore(15);
                    SoundEffectManager.Instance.PlayBagSound(this.transform);

                    // Mark this bed not empty
                    entranceWay.allBed[playerStatus.currentCorpseID - 1] = true;

                    // Clear Corpse from player
                    playerStatus.isCarryCorpse = false;
                    playerStatus.currentCorpseID = 0;

                    // Show Corpse
                    corpseOnBed.ShowCorpseOnBed();

                    // Reset to normal animation
                    playerController.ChangeAnimationLayer(false);
                }
                else
                {
                    Debug.Log("Wrong Bed!");
                    playerInteraction.ShowWrongPopup();
                }
            }
            else
            {
                if (exitWay.hasOrderCorpse)
                {
                    if (exitWay.orderCorpseID == bedID)
                    {
                        Debug.Log("Order this bed");
                        SoundEffectManager.Instance.PlayBagSound(this.transform);

                        // Mark this bed empty
                        entranceWay.allBed[bedID - 1] = false;

                        // Add Corpse to player
                        playerStatus.isCarryCorpse = true;
                        playerStatus.currentCorpseID = bedID;

                        // Clear Corpse from bed
                        corpseOnBed.HideCorpseOnBed();

                        // Reset to normal animation
                        playerController.ChangeAnimationLayer(true);
                    }
                    else
                    {
                        Debug.Log("not order");
                    }
                }
            }
        }
        else
        {
            Debug.Log("Turn on light first!");
            playerInteraction.ShowNeedLightPopup();
        }

        if (corpseOnBed.corpseOpen)
        {
            if (playerStatus.isCarryCorpse == false)
            {
                // Close Corpse
                corpseOnBed.corpseOpen = false;
                corpseOnBed.corpseTimer = 0;
                corpseOnBed.spriteRenderer.sprite = corpseOnBed.corpseSpite[0];
                corpseOnBed.StopAllCoroutines();

                SoundEffectManager.Instance.PlayBagSound(this.transform);

                Destroy(corpseOnBed.tempIndicator);

                corpseOnBed.startIndicator = false;
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
