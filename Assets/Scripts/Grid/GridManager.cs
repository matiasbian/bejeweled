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
                grid.SetValue(i,j, Cell.GetRandomCell(i, j));
            }
        }
        return grid;
    }

    public void CellClicked (CellUI cell) {

        if (!fstPickedCell) {
            fstPickedCell = cell;
        } else {
            sndPickedCell = cell;
            SpawCellPositions(fstPickedCell, sndPickedCell);

   

            bool firstCellGeneratesConn = grid.GetConnectedLines(fstPickedCell.GetCell()).Count > 2;
            bool sndCellGeneratesConn = grid.GetConnectedLines(sndPickedCell.GetCell()).Count > 2;
            
            if (!firstCellGeneratesConn && !sndCellGeneratesConn) {
                Debug.LogWarning("This swipe is not generating a new connection, undoing");
                SpawCellPositions(fstPickedCell, sndPickedCell);
            } else {
                CheckConnections();
            }
            fstPickedCell = null;
            sndPickedCell = null;

            
        }
    }

    void CheckConnections () {
        grid.ForEachElement(c => {
            var conn = grid.GetConnectedLines(c);
            conn.ForEach(c => c.DisableCell());
        });
    }

    void PrintCellConnections (Cell a) {
        Debug.Log("CELL: " + a + "----------");
    }

    void SpawCellPositions (CellUI a, CellUI b) {
        if (!grid.IsNeighbour(a.GetCell(), b.GetCell())) {
            return;
        }
        grid.CellSwaping(a.GetCell(), b.GetCell());

        int aSilb = a.transform.GetSiblingIndex();
        int bSilb = b.transform.GetSiblingIndex();

        a.transform.SetSiblingIndex(bSilb);
        b.transform.SetSiblingIndex(aSilb);
    }
}
