using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceWay : MonoBehaviour
{
    Animator animator;

    public AudioClip Bell;
    public AudioSource ASource;

    public GameObject player;

    public RecieveCorpseIndicator recieveCorpseIndicator;
    public bool startIndicator;

    public GameObject deadBodyPrefab;

    public bool hasCorpse;

    public int minSpawnRate = 5;
    public int maxSpawnRate = 10;

    [Header("Bed")]
    public bool[] allBed;
    public List<int> bedEmpty;
    public List<int> bedWithCorpseLost;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // Random Spawn Dead Body
        InvokeRepeating(nameof(SpawnDeadBody), 1f, Random.Range(minSpawnRate, maxSpawnRate + 1));
        ASource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hasCorpse)
        {
            // Start Indicator
            if (startIndicator == false)
            {
                startIndicator = true;
                recieveCorpseIndicator.DecreaseIndicator();
            }

            // Show Indicator on UI Canvas
            if (Vector2.Distance(transform.position, player.transform.position) > 2.5f)
            {
                recieveCorpseIndicator.onUI.SetActive(true);
                recieveCorpseIndicator.onObject.SetActive(false);
            }
            // Show Indicator on Object
            else
            {
                recieveCorpseIndicator.onUI.SetActive(false);
                recieveCorpseIndicator.onObject.SetActive(true);
                recieveCorpseIndicator.onObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            }
        }
        else
        {
            recieveCorpseIndicator.onUI.SetActive(false);
            recieveCorpseIndicator.onObject.SetActive(false);
        }
    }

    private void SpawnDeadBody()
    {
        if (!hasCorpse)
        {
            // Clear Previous Value
            bedEmpty.Clear();

            // Find Empty Bed
            for (int i = 0; i < allBed.Length; i++)
            {
                if (allBed[i] == false)
                {
                    // Found Empty Bed, Add to list
                    bedEmpty.Add(i);
                }
            }

            // Must have empty bed to spawn
            if (bedEmpty.Count > 0)
            {
                hasCorpse = true;

                animator.CrossFade("Enter", 0.2f);

                // Spawn Dead Body
                GameObject deadBodyClone = Instantiate(deadBodyPrefab, transform.position, transform.rotation);
                
                // Play sound to notice player
                ASource.clip = Bell;
                ASource.Play();

                // Random Empty Bed
                int randomEmptyBedNumber = Random.Range(0, bedEmpty.Count);

                // Set Number to dead body
                deadBodyClone.GetComponent<Corpse>().corpseID = bedEmpty[randomEmptyBedNumber] + 1;

                // Update UI
                recieveCorpseIndicator.incomingTextOnUI.text = "New Corpse NO." + deadBodyClone.GetComponent<Corpse>().corpseID;
                recieveCorpseIndicator.incomingTextOnObject.text = "New Corpse NO." + deadBodyClone.GetComponent<Corpse>().corpseID;
            }
            else
            {
                Debug.Log("All bed are full");
            }
        }
    }
}
