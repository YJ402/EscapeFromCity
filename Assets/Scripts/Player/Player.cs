using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Navigation,
    UI
}

public class Player : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public PlayerController controller;
    public PlayerCondition condition;
    public Interaction interaction;
    public PlayerInventory inventory;

    public PlayerState playerState;


    private void Start()
    {
        playerState = PlayerState.Navigation;
    }

    public void Init()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        interaction = GetComponent<Interaction>();
        inventory = GetComponent<PlayerInventory>();
    }
}
