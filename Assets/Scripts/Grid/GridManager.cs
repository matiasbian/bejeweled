using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public CellUI cellPrefab;
    public Transform cellsContainer;
    Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(8,8);
        grid = RandomGridFill(grid);
        grid.ForEachElement(c => {
            var instance = Instantiate<CellUI>(cellPrefab, parent: cellsContainer);
            instance.SetCell(c);
        });
        Debug.Log(grid);
    }

    public Grid RandomGridFill (Grid grid) {
        for (int i = 0; i < grid.GetRowsAmount(); i++) {
            for (int j = 0; j < grid.GetColumnsAmount(); j ++) {
                grid.SetValue(i,j, Cell.GetRandomCell());
            }
        }
        return grid;
    }
}
