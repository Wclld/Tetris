using System;
using UnityEngine;

public class Bloc : MonoBehaviour 
{
    [SerializeField]
    private Vector3 spawnPos;
    
    public virtual void Rotate() 
    {
        transform.Rotate(new Vector3(0,0,90));
    }

    public void SetToStartPosition()
    {
        transform.localPosition = spawnPos;
    }
} 