using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    float prevMouseX;
    float prevMouseY;
    public KeyCode jumpKey = KeyCode.Space;
    bool jumpPressed = false;

    public event Action<float, float> OnMousePositionChange;
    public event Action<float, float> OnMovementInput;
    public event Action OnJumpKey;
    public event Action<Vector2> OnMouseScroll;
    public event Action OnMouseRightClick;

    public void InputUpdate()
    {
        CheckInputs(); 
    }

    private void CheckInputs()
    {
        jumpPressed = false;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (prevMouseX != mouseX || prevMouseY != mouseY) OnMousePositionChange?.Invoke(mouseX, mouseY);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        OnMovementInput?.Invoke(horizontalInput, verticalInput);

        if (Input.GetKey(jumpKey))
        {
            jumpPressed = true;
            OnJumpKey?.Invoke();
        }

        Vector2 scrollDelta = Input.mouseScrollDelta;
        OnMouseScroll?.Invoke(scrollDelta);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouseRightClick?.Invoke();
        }

/*        Debug.Log($"[Input Update] Mouse ({mouseX}, {mouseY}) | Movement ({horizontalInput}, {verticalInput}) | Jumped This Frame : {jumpPressed} \n" +
            $" Scroll Delta {scrollDelta}");*/
    }
}


[CreateAssetMenu(menuName = "Scriptable Objects/Player Properties")]
public class PlayerProperties : ScriptableObject
{
    [Header("Camera")]
    [SerializeField]public float CameraSensX;
    [SerializeField]public float CameraSensY;

    [Space(15)]
    [Header("Player Movement")]
    [SerializeField] public float MovementSpeed;
    [SerializeField] public float GroundDragForce;
    [SerializeField] public float PlayerHeight;
    [SerializeField] public LayerMask GroundLayerMask;

    [SerializeField] public float JumpForce;
    [SerializeField] public float JumpCooldown;
    [SerializeField] public float AirMultiplier;
}
