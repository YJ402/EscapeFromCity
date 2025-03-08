using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Vector2 mouseDelta;
    //Vector2 currentCameraRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mouse);  
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    //public void OnLook(InputAction.CallbackContext context)
    //{
    //    Debug.Log("..");
    //    mouseDelta = context.ReadValue<Vector2>();
    //}

    public void OnDirection(InputAction.CallbackContext context)
    {
        Debug.Log("..");
    }
}
