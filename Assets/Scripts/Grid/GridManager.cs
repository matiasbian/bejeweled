using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridManager : MonoBehaviour
{
    public CellUI cellPrefab;
    public Transform cellsContainer;
    public GameObject generatingBoardText;
    Animation anim;
    Grid grid;

    CellUI fstPickedCell;
    CellUI sndPickedCell;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        generatingBoardText.SetActive(true);
        transform.localScale = Vector3.zero;
        grid = new Grid(8,8);
        CellUI.onCellClicked += CellClicked;
        SimpleSwipeDetection.onSwipe += OnSwipe;
        GenerateRandomGrid();
    }

    void GenerateRandomGrid () {
        grid.RandomGridFill();
        grid.ForEachElement(c => {
            var instance = Instantiate<CellUI>(cellPrefab, parent: cellsContainer);
            instance.SetCell(c);
        });

        StartCoroutine(CheckConnections(() => {
            generatingBoardText.SetActive(false);
            anim.Play();
        }));
    }

    

    public void CellClicked (CellUI cell) {
        cell.SelectCell();
        if (!fstPickedCell) {
            fstPickedCell = cell;
        } else {
            sndPickedCell = cell;
            TwoCellsPickedActions(fstPickedCell, sndPickedCell);
            ResetButtonsState();
        }
    }

    IEnumerator CheckConnections (Action finishCallback = null) {
        bool noConn = false;
        while (!noConn) {
            HashSet<Cell> toSlideDown = new HashSet<Cell>();
            noConn = true;
            for (int i = 0; i < grid.width; i++) {
                for (int j = 0; j < grid.height; j++) {
                    var c = grid.GetCell(i,j);
                    var conn = grid.GetConnectedLines(c);
                    if (conn.Count > 0 ) {
                        noConn = false;
                        toSlideDown.UnionWith(conn);
                    }
                }
            }
            List<IEnumerator> cellCoro = new List<IEnumerator>();
            foreach (var c in toSlideDown) cellCoro.Add(SlideDownPieces(c));

            yield return this.WaitAll(cellCoro);
        }
        finishCallback?.Invoke();
        yield break;
    }

    public bool checkAll;
    public bool parcialConn;

    void TwoCellsPickedActions (CellUI fst, CellUI snd) {
        bool areNeighb = SpawCellPositions(fst, snd);

        if (!areNeighb) return;

        bool firstCellGeneratesConn = grid.GetConnectedLines(fst.GetCell()).Count > 2;
        bool sndCellGeneratesConn = grid.GetConnectedLines(snd.GetCell()).Count > 2;
        
        if (!firstCellGeneratesConn && !sndCellGeneratesConn) {
            Debug.LogWarning("This swipe is not generating a new connection, undoing");
            StartCoroutine(ReturnPieces(fst,snd));
        } else {
            StartCoroutine(CheckConnections());
        }
    }



    void OnSwipe (Vector2Int dir, CellUI fstCell) {
        var sndCell = grid.GetCell(fstCell.GetCell().x - dir.y, fstCell.GetCell().y + dir.x);
        TwoCellsPickedActions(fstCell, sndCell.ui);
    }

    void ResetButtonsState () {
        fstPickedCell.UnselectCell();
        sndPickedCell.UnselectCell();
        fstPickedCell = null;
        sndPickedCell = null;
    }

    bool SpawCellPositions (CellUI a, CellUI b) {
        if (!grid.IsNeighbour(a.GetCell(), b.GetCell())) {
            return false;
        }
        grid.CellSwaping(a.GetCell(), b.GetCell());

        int aSilb = a.transform.GetSiblingIndex();
        int bSilb = b.transform.GetSiblingIndex();

        a.transform.SetSiblingIndex(bSilb);
        b.transform.SetSiblingIndex(aSilb);
        return true;
    }

    IEnumerator SlideDownPieces (Cell cell) {

        Cell cellOver = grid.GetOverCell(cell);
        cell.HideCell();
        yield return new WaitForSeconds(0.3f);
        while (cellOver != null) {
            SpawCellPositions(cell.ui, cellOver.ui);
            cellOver = grid.GetOverCell(cell);
            yield return new WaitForSeconds(0.05f);
        }
        cell.EnableCell();
        cell.CreateNewOne();
        
        yield break;
    }

    IEnumerator ReturnPieces (CellUI a, CellUI b) {
        a.ShowError();
        b.ShowError();

        yield return new WaitForSeconds(0.6f);
        SpawCellPositions(a, b);
        a.ClearError();
        b.ClearError();
        yield break;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    public bool reset;
    void Update()
    {
        if (reset) {
            reset = false;
            grid.ForEachElement(c => {
                c.ResetCell();
            });
        }
    }
}
