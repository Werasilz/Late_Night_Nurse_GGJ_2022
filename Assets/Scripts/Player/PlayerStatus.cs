using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    GameManager gameManager;

    private PlayerController movement;

    [Header("Corpse")]
    public bool isCarryCorpse;
    public int currentCorpseID;

    [Header("Health")]
    public GameObject hpBar;
    public GameObject hpPointPrefab;
    public int hp = 5;

    [Header("Score")]
    public int score;
    public GameObject scoreUI;
    public TextMeshProUGUI scoreText;
    public GameObject popupScore;

    public GameObject gameOverMenu;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        movement = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            gameManager.counting = false;

            // Show Game over menu
            gameOverMenu.SetActive(true);
            
            //stop music
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        }
        
        Carrying();
    }

    public void DecreaseHealth()
    {
        if (hp > 0)
        {
            hp -= 1;
            Destroy(hpBar.transform.GetChild(0).gameObject);
        }
    }

    public void GetScore(int newScore)
    {
        score += newScore;
        scoreText.text = "SCORE : " + score.ToString();
        popupScore.SetActive(true);
        popupScore.GetComponent<TextMeshProUGUI>().text = "+" + newScore;
        Invoke(nameof(HidePopupScore), 0.5f);
    }

    private void HidePopupScore()
    {
        popupScore.SetActive(false);
    }

    public void GetHP()
    {
        if (hp < 5)
        {
            hp += 1;
            GameObject hpPoint = Instantiate(hpPointPrefab, hpBar.transform);
            // hpPoint.transform.SetParent(hpBar.transform);
        }
    }

    public void Carrying()
    {
        if (!isCarryCorpse)
        {
            movement.moveSpeed = 3;
        }
        else
        {
            movement.moveSpeed = 2;
        }
    }
}
