using UnityEngine;

public class SBloc : Bloc 
{
    private int multiplier = 1;
    public override void Rotate()
    {
        var rotationAngle = 90 * multiplier;
        transform.Rotate(new Vector3(0,0,90 * rotationAngle));
        multiplier *= -1;
    }
}