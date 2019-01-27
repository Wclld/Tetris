using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;

public class BlocsSpawner : MonoBehaviour // declaring class and inheriting it from MonoBehaviour, so this class can be attached to GameObject
{
    [SerializeField] // adding do field attribute, so it can be changed in Unity's inspector  
    private GameObject[] blocs; // declaring an array of prefabs

    private GameObject nextFigure;
    private readonly Vector3 nextBlocPosition = new Vector3(11,22,0);


    public Bloc SpawnBloc() // declaring a public method that can be accessed from other class
    {
        Bloc bloc;
        if (blocs.Length <= 0) 
            return null;
        
        if (nextFigure != null)
        {
            bloc = nextFigure.GetComponent<Bloc>();
        }
        else
        {
            var randomedValue = Random.Range(0, blocs.Length);
            bloc = Instantiate(blocs[randomedValue]).GetComponent<Bloc>();
        }
        
        bloc.SetToStartPosition();
        
        var nextFigureIndex = Random.Range(0, blocs.Length);
        nextFigure = Instantiate((blocs[nextFigureIndex]));
        nextFigure.transform.position = nextBlocPosition;
        
        return bloc;
    }
}
