using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public Vector2Int pos;
    public int gCost;
    public int hCost;
    public GridNode parent;

    public int fCost => gCost + hCost;

    public GridNode(Vector2Int pos)
    {
        this.pos = pos;
        gCost = int.MaxValue;
    }
}


//its a grid so