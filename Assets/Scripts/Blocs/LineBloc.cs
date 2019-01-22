using UnityEngine;

public class LineBloc : Bloc //inheriting from parrent class
{
    private int multiplier = 1; //creating private variable

    public override void Rotate() //overriding inherited method, so it executes other logic 
    {
        var rotationAngle = 90 * multiplier; // creating new variable which type depends on right side part
        transform.Rotate(new Vector3(0,0,90 * rotationAngle)); //using multiplier to change value
        multiplier *= -1;
    }
}