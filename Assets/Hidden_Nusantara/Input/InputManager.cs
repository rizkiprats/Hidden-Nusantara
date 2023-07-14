using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Manager", menuName = "Input Manager")]
public class InputManager : ScriptableObject, PlayerControls.IPlayerActions
{
    private PlayerControls input;
    public UnityAction<Vector2> OnMoveAction;
    public UnityAction OnPauseAction;
    public UnityAction OnJumpAction;
    public UnityAction OnInteractAction;
    void OnEnable()
    {
        if (input == null)
            input = new PlayerControls();
        input.Player.SetCallbacks(this);
        input.Player.Enable();
    }
    void OnDisable()
    {
        input.Player.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)
        {
            OnMoveAction?.Invoke(context.ReadValue<Vector2>());
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        OnJumpAction?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke();
    }

}
