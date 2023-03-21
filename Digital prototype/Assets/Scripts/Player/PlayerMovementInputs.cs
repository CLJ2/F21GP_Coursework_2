using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This class creates an interface between the input manager and the player
 * This allows us to control and modify the inputs. 
 * It handles all messages from the input manager and stores them for the player to access only when it needs them instead of every time they are updated
 */
public class PlayerMovementInputs : MonoBehaviour
{
    private Vector2 move;
    private Vector2 look;
    private bool jump;
    private bool sprint;
    private bool useAbility;

    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    //These On functions recieve data from the input manager and store it
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jump = value.isPressed;
    }

    public void OnSprint(InputValue value)
    {
        sprint = value.isPressed;
    }

    public void OnUseAbility(InputValue value)
    {
        useAbility = value.isPressed;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    //Getters allow other code to access the information in this object
    public Vector2 GetMove()
    {
        return move;
    }

    public Vector2 GetLook()
    {
        return look;
    }

    public bool GetJump()
    {
        return jump;
    }

    public bool GetSprint()
    {
        return sprint;
    }

    public bool GetUseAbility()
    {
        return useAbility;
    }

    public void SetJump(bool value)
    {
        jump = value;
    }

    public void SetUseAbility(bool value)
    {
        useAbility = value;
    }
}
