using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public CellUI cellPrefab;
    public Transform cellsContainer;
    Grid grid;

    CellUI fstPickedCell;
    CellUI sndPickedCell;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(8,8);
        grid = RandomGridFill(grid);
        grid.ForEachElement(c => {
            var instance = Instantiate<CellUI>(cellPrefab, parent: cellsContainer);
            instance.SetCell(c);
        });
        CellUI.onCellClicked += CellClicked;
    }



    public Grid RandomGridFill (Grid grid) {
        for (int i = 0; i < grid.GetRowsAmount(); i++) {
            for (int j = 0; j < grid.GetColumnsAmount(); j ++) {
                grid.SetValue(i,j, Cell.GetRandomCell(j, i));
            }
        }
        return grid;
    }

    public void CellClicked (CellUI cell) {
        Debug.Log("Cell clicked " + cell.GetCell());

        if (!fstPickedCell) {
            fstPickedCell = cell;
        } else {
            sndPickedCell = cell;
            Debug.Log("FIRST PICK: " + fstPickedCell.GetCell() + " SECOND PICK " + sndPickedCell.GetCell());
            SpawCellPositions(fstPickedCell, sndPickedCell);
            PrintCellConnections(fstPickedCell.GetCell());
            PrintCellConnections(sndPickedCell.GetCell());
            fstPickedCell = null;
            sndPickedCell = null;
        }


        //var finalList = grid.GetAllConectedCells(cell.x,cell.y, new HashSet<Cell>());
        //foreach(var e in finalList) Debug.Log(e);
    }

    void PrintCellConnections (Cell a) {
        Debug.Log("CELL: " + a + "----------");
        var conn = grid.GetAllConectedCells(a.x, a.y, new HashSet<Cell>());
        if (conn.Count > 2) {
            Debug.Log("THERE'S A CONNECTION!");
            foreach (var c in conn) Debug.Log("conn: " + c);
        }
    }

    void SpawCellPositions (CellUI a, CellUI b) {
        if (!grid.IsNeighbour(a.GetCell(), b.GetCell())) return;
        int aPos = a.transform.GetSiblingIndex();
        int bPos = b.transform.GetSiblingIndex();


        int aX = a.GetCell().x;
        int aY = a.GetCell().y;

        int bX = b.GetCell().x;
        int bY = b.GetCell().y;

        grid.CellSwaping(aX, aY, bX, bY);
        
        a.transform.SetSiblingIndex(bPos);
        a.GetCell().UpdatePos(bX, bY);
        b.transform.SetSiblingIndex(aPos);
        b.GetCell().UpdatePos(aX,aY);

        
    }
}
