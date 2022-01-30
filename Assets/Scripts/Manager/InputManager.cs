using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInteraction playerInteraction;

    public Vector2 move;
    public bool E_Input;

    private void Awake()
    {
        playerInteraction = FindObjectOfType<PlayerInteraction>();
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.Move.performed += playerInput => move = playerInput.ReadValue<Vector2>();
            playerInput.Player.Interact.performed += playerInput => E_Input = true;
        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        // Press E to interact
        if (E_Input)
        {
            if (playerInteraction.interactable != null)
            {
                playerInteraction.interactable.Interact(playerInteraction);
            }
        }
    }

    private void LateUpdate()
    {
        E_Input = false;
    }
}
