using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("..");
        //mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        Debug.Log("..");
    }
}
