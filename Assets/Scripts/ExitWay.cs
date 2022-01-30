using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWay : MonoBehaviour
{
    EntranceWay entranceWay;
    PlayerStatus playerStatus;

    public GameObject player;
    public SentCorpseIndicator sentCorpseIndicator;
    public bool startIndicator;

    public bool hasOrderCorpse;
    public int orderCorpseID;
    public float orderTimer;
    public float oderLimit = 15;
    public List<int> corpseBed;
    private bool orderCorpseIsLost;

    public int minOrderRate = 20;
    public int maxOrderRate = 30;
    
    public AudioClip Bell;
    public AudioSource ASource;

    private void Awake()
    {
        entranceWay = FindObjectOfType<EntranceWay>();
        playerStatus = FindObjectOfType<PlayerStatus>();
    }

    private void Start()
    {
        // Random Order Corpse
        InvokeRepeating(nameof(RequestCorpse), 1f, Random.Range(minOrderRate, maxOrderRate + 1));
        
        ASource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hasOrderCorpse)
        {
            // Start Indicator
            if (startIndicator == false)
            {
                startIndicator = true;
                sentCorpseIndicator.DecreaseIndicator();
                orderTimer = 0;
            }

            // Show Indicator on UI Canvas
            if (Vector2.Distance(transform.position, player.transform.position) > 2.5f)
            {
                sentCorpseIndicator.onUI.SetActive(true);
                sentCorpseIndicator.onObject.SetActive(false);
            }
            // Show Indicator on Object
            else
            {
                sentCorpseIndicator.onUI.SetActive(false);
                sentCorpseIndicator.onObject.SetActive(true);
                sentCorpseIndicator.onObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            }

            orderTimer += Time.deltaTime;

            if (orderTimer > oderLimit)
            {
                playerStatus.DecreaseHealth();
                hasOrderCorpse = false;
                startIndicator = false;
                orderCorpseID = 0;
            }
        }
        else
        {
            sentCorpseIndicator.onUI.SetActive(false);
            sentCorpseIndicator.onObject.SetActive(false);
        }
    }

    private void RequestCorpse()
    {
        if (!hasOrderCorpse)
        {
            Debug.Log("START REQUEST CORPSE");
            corpseBed.Clear();
            orderCorpseIsLost = false;

            // Find Corpse Bed
            for (int i = 0; i < entranceWay.allBed.Length; i++)
            {
                if (entranceWay.allBed[i])
                {
                    corpseBed.Add(i);
                }
            }

            if (corpseBed.Count > 0)
            {
                // Random Corpse Bed
                int randomCorpseBedNumber = Random.Range(0, corpseBed.Count);

                for (int i = 0; i < entranceWay.bedWithCorpseLost.Count; i++)
                {
                    if (corpseBed[randomCorpseBedNumber] == entranceWay.bedWithCorpseLost[i])
                    {
                        Debug.Log("random order is same as corpse lost");
                        orderCorpseIsLost = true;
                        return;
                    }
                }

                if (orderCorpseIsLost) return;

                hasOrderCorpse = true;
                
                ASource.clip = Bell;
                ASource.Play();

                // Set Number to dead body
                orderCorpseID = corpseBed[randomCorpseBedNumber] + 1;

                // Update UI
                sentCorpseIndicator.incomingTextOnUI.text = "Corpse Requset NO." + orderCorpseID;
                sentCorpseIndicator.incomingTextOnObject.text = "Corpse Requset NO." + orderCorpseID;
            }
            else
            {
                Debug.Log("All bed are empty");
            }
        }
    }
}
