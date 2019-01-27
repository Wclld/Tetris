using System;
using UnityEngine;

public class Bloc : MonoBehaviour 
{
    [SerializeField]
    private Vector3 spawnPos;
    
    public virtual void Rotate(bool reverseRotate = false)
    {
        float rotationAngle = 90;
        if (reverseRotate)
            rotationAngle *= -1;            
        transform.Rotate(new Vector3(0,0,rotationAngle));
    }

    public void SetToStartPosition()
    {
        transform.localPosition = spawnPos;
    }
} 