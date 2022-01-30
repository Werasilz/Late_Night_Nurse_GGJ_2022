using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    GameManager gameManager;

    public float moveSpeed = 3;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = FindObjectOfType<InputManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.finishLevel == false)
        {
            animator.SetFloat("Horizontal", inputManager.move.x);
            animator.SetFloat("Vertical", inputManager.move.y);

            if (inputManager.move != Vector2.zero)
            {
                transform.Translate(inputManager.move * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void ChangeAnimationLayer(bool change)
    {
        if (change)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}
