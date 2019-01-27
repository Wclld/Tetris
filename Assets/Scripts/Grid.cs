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
        bricks = new Transform[height, width];
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
            if (bricks[brickPosition.y, brickPosition.x] != null)
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
                bricks[brickPosition.y -1, brickPosition.x] != null)
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
            if (brick.y > height)
            {
                GameManager.Instance.
            }
            bricks[brick.y, brick.x] = bloc.GetChild(i);
        }
        CheckLines();
    }

    private void ShowInfo()
    {
        Debug.Log("ShowingInfo");
        for (int i = 0; i < height; i++)
        {
            var row = "";
            for (int j = 0; j < width; j++)
            {
                if (bricks[i, j] != null)
                    row += "1 ";
                else
                    row += "0 ";
            }
            Debug.Log(row);
        }
    }

    private void CheckLines()
    {
        Debug.Log("Called CheckLine");
        for (int i = 0; i < height;)
        {
            if (!CheckLine(i))
                i++;
        }
    }

    private bool CheckLine(int lineNumber)
    {
        for (int j = 0; j < width; j++)
        {
            if (bricks[lineNumber, j] == null)
            {
                Debug.LogFormat("{0} {1}", lineNumber, j);
                return false;
            }
        }
        RemoveLine(lineNumber);
        MoveLinesDown(lineNumber);
        ShowInfo();
        return true;
    }

    private void RemoveLine(int line)
    {
        for (int i = 0; i < width; i++)
        {
            Destroy(bricks[line, i].gameObject);
            bricks[line, i] = null;
        }
        Debug.Log("qwe");
    }

    private void MoveLinesDown(int line)
    {
        for (int i = line; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (bricks[i, j] != null)
                {
                    var pos = bricks[i, j].position - new Vector3(0, 1, 0);
                    bricks[i, j].position = pos;
                    bricks[i - 1, j] = bricks[i, j];
                    bricks[i, j] = null;
                }
            }
        }
    }
}
