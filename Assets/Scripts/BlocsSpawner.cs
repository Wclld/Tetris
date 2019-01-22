﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocsSpawner : MonoBehaviour // declairing class and inheriting it from MonoBehaviour, so this class can be attached to GameObject
{
    [SerializeField] // adding do field attribute, so it can be changed in Unity's inspector  
    private GameObject[] blocs; // declairing an array of prefabs
    public void SpawnBlock() // declaring a public method that can be accessed from other class
    {
        if(blocs.Length > 0) // checking if there any value in array
        {
            var randomedIterator = Random.Range(0,blocs.Length); // generating a value between 0 and number of elements on array
            var newBloc = Instantiate(blocs[randomedIterator]).GetComponent<Bloc>(); // instantiating object from array and getting attached component
            newBloc.SetToStartPosition(); // calling accessible method 
        }
    }
}
