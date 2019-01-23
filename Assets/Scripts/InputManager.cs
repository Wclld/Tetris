using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour // inheriting MonoBehaviour class, so this class can be attached to GameObject
{
    public event Action<MoveSide> OnLeftPressed = x => { }; 
    public event Action<MoveSide> OnRightPressed = x => { }; 
    public event Action OnUpPressed = () => { }; 
    public event Action OnDownPressed = () => { }; 
    private void Update() // using MonoBehaviours Message method that is called once per frame
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
}
