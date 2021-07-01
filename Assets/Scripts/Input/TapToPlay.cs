using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapToPlay : MonoBehaviour
{

    void Update()
    {
        if (PlayerInput())
        {
            HandleInput();
        }
    }

    private bool PlayerInput()
    {
        return Input.touchCount > 0 || Input.GetMouseButtonDown(0);
    }

    private void HandleInput()
    {
        if (UIButtonClicked()) { return; }
        StateMachine.instance.SwitchGameState(GameStates.Playing);
    }
#if UNITY_EDITOR
    private bool UIButtonClicked()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
#else
    private bool UIButtonClicked()
    {
        if (Input.touches.Length < 1) { return false; }
        return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
    }
#endif
}
