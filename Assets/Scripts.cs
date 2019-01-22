using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private int height = 20;
    [SerializeField]
    private int width = 10;
    private int bottomBlockYPos = -11;

    [ContextMenu("BuildGlass")]
    private void Build()
    {
        if(prefab != null && parent != null)
        {
            BuildBottom();
            BuildSides();
        }
        else
        {
            print("Someone is null");
        }
    }

    private void BuildSides()
    {
        var halfHeight = height / 2;
        var leftXPos = -0;
        var rightXPos = width + 1;
        for (int yPos = 0; yPos <= height; yPos++)
        {
            var leftBlockPart = Instantiate(prefab, parent);
            var rightBlockPart = Instantiate(prefab, parent);
            leftBlockPart.transform.localPosition = new Vector3(leftXPos, yPos, 0);
            rightBlockPart.transform.localPosition = new Vector3(rightXPos, yPos, 0);
        }
    }

    private void BuildBottom()
    {
        for (float xPos = 0; xPos <= width; xPos++)
        {
            var blockPart = Instantiate(prefab, parent);
            blockPart.transform.localPosition = new Vector3(xPos, 0, 0);
        }
    }
}
