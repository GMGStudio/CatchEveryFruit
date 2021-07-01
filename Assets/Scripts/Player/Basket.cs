using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Vector3 inputPosition;
    bool touched;

    private void Update()
    {
        if (InputStarted())
        {
            touched = true;
        }
        else if (InputEnded())
        {
            touched = false;
        }

        if (touched)
        {
            MoveBasket();
        }
    }

    void MoveBasket()
    {
        Vector3 targetPosition = transform.position;
        if (TouchInput())
        {
            inputPosition = GetCursorPosition(Input.GetTouch(0).position);
        }
        else
        {
            inputPosition = GetCursorPosition(Input.mousePosition);
        }
        targetPosition.x = inputPosition.x;

        float step = 30 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    Vector3 GetCursorPosition(Vector3 input)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, input.z));
    }


    private bool InputStarted()
    {
        return TouchInput() || MouseInput();
    }

    private bool InputEnded()
    {
        return TouchEnded() || MouseEnded();
    }

    private bool TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //Only First touch
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    private bool TouchEnded()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //Only First touch
            if (touch.phase == TouchPhase.Ended)
            {
                return true;
            }
        }
        return false;
    }

    private bool MouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool MouseEnded()
    {
        return Input.GetMouseButtonUp(0);
    }

}
