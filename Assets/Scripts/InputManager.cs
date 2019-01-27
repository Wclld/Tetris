using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour // inheriting MonoBehaviour class, so this class can be attached to GameObject
{

    private Vector3 dragEndPos;
    private Vector3 dragStartPos;

    
    private bool inGame = false;
    public event Action<MoveSide> OnLeftPressed = x => { }; 
    public event Action<MoveSide> OnRightPressed = x => { }; 
    public event Action OnUpPressed = () => { }; 
    public event Action OnDownPressed = () => { };

    private void Start()
    {
        GameManager.Instance.OnGameOver += () => { inGame = !inGame; };
        GameManager.Instance.OnGameStarted += () => { inGame = !inGame; };
    }

    private void Update() // using MonoBehaviours Message method that is called once per frame
    {
        if (inGame)
        {
//            CheckInput();
            CheckMobileInput();
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // checking for input of specific key
        {
            OnLeftPressed.Invoke(MoveSide.Left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightPressed.Invoke(MoveSide.Right);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUpPressed.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDownPressed.Invoke();
        }
    }

    private void CheckMobileInput()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                dragStartPos = touch.position;
                dragEndPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                dragEndPos = touch.position;
                DetectDirection();
            }
        }
    }

    private void DetectDirection()
    {
        var vertDistance = Mathf.Abs(dragEndPos.y - dragStartPos.y);
        var horDistance = Mathf.Abs(dragEndPos.x - dragStartPos.x);

        if (vertDistance > horDistance)
        {
            var swipeAction = dragEndPos.y - dragStartPos.y > 0 ? OnUpPressed : OnDownPressed;
            swipeAction?.Invoke();
        }
        else
        {
            if (dragEndPos.x - dragStartPos.x > 0)
            {
                OnRightPressed(MoveSide.Right);
            }
            else
            {
                OnLeftPressed(MoveSide.Left);
            }
        }
        
    }
}
