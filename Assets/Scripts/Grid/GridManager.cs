using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(8,8);
        grid = RandomGridFill(grid);
    }

    public Grid RandomGridFill (Grid grid) {
        for (int i = 0; i < grid.grid.GetLength(0); i++) {
            for (int j = 0; j < grid.grid.GetLength(1); j ++) {
                grid.grid[i,j] = Cell.GetRandomCell();
            }
        }
        return grid;
    }
}
