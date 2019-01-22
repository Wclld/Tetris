using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BlocMover;

public class InputManager : MonoBehaviour // inheriting MonoBehaviour class, so this class can be attached to GameObject
{
    private BlocMover blocMover; // declaring private value, because this one is not needed in public access
    private void Update() // using MonoBehaviours Message method that is called once per frame
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // checking for input of specific key
        {
            blocMover.MoveAside(MoveSide.Left); // calling object's accessable method
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            blocMover.MoveAside(MoveSide.Right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            blocMover.Rotate();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            blocMover.MoveDown();
        } 
    }
}
