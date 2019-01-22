using System;
using System.Collections;
using UnityEngine;

public class BlocMover : MonoBehaviour // declairing class and inheriting it from MonoBehaviour, so this class can be attached to GameObject
{
    public float BlocStartMoveTime; // declaring public field with floating coma, that can be accessed from other class and changed through Unity's editor 
    public float MinMoveTime;
    private float currentMoveTime; // declaring private value, because this one is not needed in public access

    [SerializeField] // adding do field attribute, so it can be changed in Unity's inspector  
    private float TimeDecreasePercent = 0.01f;
    private Bloc currentBloc;
    private Coroutine movingCoroutine; 

    public void SetBloc(Bloc block) // declaring a public method that can be accessed from other class
    {
        currentBloc = block;
    }
    public void StartMoving()
    {
        movingCoroutine = StartCoroutine(MoveBlocDownRoutine()); // creating and assigning to field coroutine: a function that is executed in intervals and continues from last yield while it's not finished  
    }
    public void Stop()
    {
        if(movingCoroutine != null) // using conditional statement to check that value is not empty
            StopCoroutine(movingCoroutine); // making Coroutine stop before it finishes its work
    }

    public void MoveAside(MoveSide side)
    {
       Move(new Vector3(0,(float)side,0)); // invoking local Method passing to it new value
    }
    public void MoveDown()
    {
       Move(Vector3.down);
    }

    public void Rotate()
    {
        currentBloc?.Rotate(); // checking for object is not null and calling accessible method
    }

    private IEnumerator MoveBlocDownRoutine() // declairing function that cannot be easily accessed from other classes
    {
        yield return new WaitForSeconds(currentMoveTime); // making this function to wait for declared time
        Move(Vector3.down);
        currentMoveTime = DecreaseTime(currentMoveTime); // passing and getting value from local function  
    }

    private void Move(Vector3 direction)
    {
        if(currentBloc != null)
            currentBloc.transform.localPosition += direction;
        else
            Debug.LogError("Bloc is not set!"); // logging message th the unity console
    }
    private float DecreaseTime(float time)
    {
        var calculatedTime = time - (time * TimeDecreasePercent); 

        if(calculatedTime > MinMoveTime)
            calculatedTime = time;

        return calculatedTime; //returning value from function
    } 

    public enum MoveSide // declairing enumeration 
    {
        Left = -1, // setting custom values
        Right = 1
    }

}
