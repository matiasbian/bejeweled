using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int height;
    int width;
    public Cell[,] grid;
    public Grid (int height, int width) {
        this.height = height;
        this.width = width;
        grid = new Cell[height, width];
    }


}
