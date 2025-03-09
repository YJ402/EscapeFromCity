using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //public MainCamera camera;
    //public Camera camera;
    public FirstPersonController firstPersonController;
    public PlayerController controller;
    public PlayerCondition condition;


    void Start()
    {
        //camera = Camera.main;
        firstPersonController = Camera.main.GetComponent<FirstPersonController>();
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    void Init()
    {

    }

    void Update()
    {
    }


}
