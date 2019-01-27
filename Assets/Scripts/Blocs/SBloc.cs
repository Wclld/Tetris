using UnityEngine;

public class SBloc : Bloc 
{
    private int multiplier = 1;
    public override void Rotate(bool reverseRotate = false)
    {
        var rotationAngle = 90 * multiplier;
        if (reverseRotate)
            rotationAngle *= -1;
        transform.Rotate(new Vector3(0,0,rotationAngle));
        multiplier *= -1;
    }
}