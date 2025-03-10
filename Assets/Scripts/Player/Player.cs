using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public FirstPersonController firstPersonController;
    public PlayerController controller;
    public PlayerCondition condition;
    public Interaction interaction;
    public void Init()
    {
        firstPersonController = Camera.main.GetComponent<FirstPersonController>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        interaction = GetComponent<Interaction>();
    }
}
