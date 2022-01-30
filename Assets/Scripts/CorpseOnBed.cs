using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CorpseOnBed : MonoBehaviour
{
    EntranceWay entranceWay;
    LightPoint lightPoint;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public GameObject player;

    public CorpseOpenIndicator corpseOpenIndicator;
    public bool startIndicator;
    public GameObject tempIndicator;
    public float corpseTimer;
    public float timeToCorpseLost = 20;

    public Sprite[] corpseSpite;

    public bool corpseOpen;

    public bool hasCorpseOnBed;

    private void Awake()
    {
        entranceWay = FindObjectOfType<EntranceWay>();
        lightPoint = GetComponentInParent<LightPoint>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        HideCorpseOnBed();
    }

    public void ShowCorpseOnBed()
    {
        spriteRenderer.sprite = corpseSpite[0];
        hasCorpseOnBed = true;
        spriteRenderer.enabled = true;
    }

    public void HideCorpseOnBed()
    {
        spriteRenderer.sprite = corpseSpite[0];
        hasCorpseOnBed = false;
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        // On light turn off and has corpse on bed
        if (lightPoint.lighting == false)
        {
            if (hasCorpseOnBed)
            {
                if (corpseOpen == false)
                {
                    StartCoroutine(RandomCorpseOpen());
                }
            }
        }

        if (hasCorpseOnBed == false)
        {
            Destroy(tempIndicator);
            tempIndicator = null;
        }

        if (GetComponentInParent<Bed>().canUseBed)
        {
            // On Corpse Open
            if (corpseOpen)
            {
                // Start Indicator
                if (startIndicator == false)
                {
                    startIndicator = true;
                    corpseTimer = 0;

                    tempIndicator = Instantiate(corpseOpenIndicator.onObject, corpseOpenIndicator.transform) as GameObject;
                    // tempIndicator.transform.SetParent(corpseOpenIndicator.transform);

                    SoundEffectManager.Instance.PlayGhostSound(this.transform);

                    corpseOpenIndicator.DecreaseIndicator(tempIndicator.transform.GetChild(0).GetChild(2).GetComponent<Image>());
                    tempIndicator.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Random.Range(0, 2) == 0 ? "!@#$%^&*(" : "*^)$_@+!^";
                }

                // Show Indicator on UI Canvas
                if (Vector2.Distance(transform.position, player.transform.position) > 2.5f)
                {
                    tempIndicator.SetActive(false);
                }
                // Show Indicator on Object
                else
                {
                    tempIndicator.SetActive(true);
                    tempIndicator.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
                }

                corpseTimer += Time.deltaTime;

                // Corpse Time out
                if (corpseTimer > timeToCorpseLost)
                {
                    Debug.Log("Corpse Lost");

                    SoundEffectManager.Instance.PlayAudio(SoundEffectManager.Instance.AudioClips[0].corpseLost, this.transform.position, 0.5f);

                    // Corpse Lost
                    player.GetComponent<PlayerStatus>().DecreaseHealth();

                    Destroy(tempIndicator);

                    spriteRenderer.sprite = corpseSpite[2];
                    GetComponentInParent<Bed>().canUseBed = false;
                    entranceWay.bedWithCorpseLost.Add(GetComponentInParent<Bed>().bedID - 1);
                }
            }
            else
            {
                Destroy(tempIndicator);
            }
        }
    }

    IEnumerator RandomCorpseOpen()
    {
        yield return new WaitForSeconds(Random.Range(25, 31));

        if (hasCorpseOnBed)
        {
            // Corpse change sprite
            corpseOpen = true;
            spriteRenderer.sprite = corpseSpite[1];
        }

        StopAllCoroutines();
    }
}
