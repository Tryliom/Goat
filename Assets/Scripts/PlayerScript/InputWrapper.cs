using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWrapper : MonoBehaviour
{
    public Vector2 move;
    
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
}
