using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Extensions;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    private Transform[,] bricks;

    private void Awake()
    {
        bricks = new Transform[width, height];
    }

    public bool BlocIsInGrid(Transform bloc)
    {
        for (int i = 0; i < bloc.childCount; i++)
        {
            var brickPosition = Vector3Int.RoundToInt(bloc.GetChild(i).position) - Vector3Int.one;

            if (!brickPosition.x.IsInRange(0, width) || !brickPosition.y.IsInRange(0, height + 2))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsOnBottom(Transform bloc)
    {
        for (int i = 0; i < bloc.childCount; i++)
        {
            var brickPosition = Vector3Int.RoundToInt(bloc.GetChild(i).position) - Vector3Int.one;
            if (!brickPosition.y.IsInRange(1, height + 2) ||
                bricks[brickPosition.x, brickPosition.y - 1] != null)
            {
                AddBlocToArray(bloc);
                Debug.Log(brickPosition);
                return true;
            }
        }
        return false;
    }

    private void AddBlocToArray(Transform bloc)
    {
        for (int i = 0; i < bloc.childCount; i++)
        {
            var brick = Vector3Int.RoundToInt(bloc.GetChild(i).position) - Vector3Int.one;
            bricks[brick.x, brick.y] = bloc.GetChild(i);
        }
    }
}
