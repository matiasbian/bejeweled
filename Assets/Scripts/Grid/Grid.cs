using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Grid
{
    int height;
    int width;
    Cell[,] grid;
    public Grid (int height, int width) {
        this.height = height;
        this.width = width;
        grid = new Cell[height, width];
    }

    public int GetRowsAmount () {
        return height;
    }

    public int GetColumnsAmount () {
        return width;
    }

    public void SetValue (int x, int y, Cell value) {
        grid[x,y] = value;
        value.UpdatePos(x,y);
    }

    public void ForEachElement (Action<Cell> callback) {
        for (int i = 0 ; i < width; i++) {
            for (int j = 0; j < height; j++) {
                callback?.Invoke(GetCell(i,j));
            }
        }
    }

    public Cell GetCell (int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height) return null;
        return grid[x,y];
    }

    public void CellSwaping (Cell a, Cell b) {
        int ax = a.x;
        int ay = a.y;

        int bx = b.x;
        int by = b.y;

        SetValue(ax, ay, b);
        SetValue(bx, by, a);
    }

    List<Cell> GetNeighboringCells (Cell cell) {
        List<Cell> cells = new List<Cell>();
        
        cells.AddIfNotNull(GetCell(cell.x - 1, cell.y));
        cells.AddIfNotNull(GetCell(cell.x + 1, cell.y));
        cells.AddIfNotNull(GetCell(cell.x, cell.y - 1));
        cells.AddIfNotNull(GetCell(cell.x, cell.y + 1));
        return cells;
    }

    public bool IsNeighbour (Cell a, Cell b) {
        //GetNeighboringCells(a).ForEach(v => Debug.Log("vecino de " + a + " : | " + v + " | yo pregunto por " + b));
        return GetNeighboringCells(a).Contains(b);
    }

    public List<Cell> GetNeighboringCellsOfSameType (Cell cell) {
        var cells = GetNeighboringCells(cell);
        return GetNeighboringCells(cell).FindAll(nCell => nCell.type == cell.type);
    }

    public List<Cell> GetConnectedLines (Cell cell) {
        var result = new List<Cell>();
        result.Add(cell);

        var neigh = GetNeighboringCellsOfSameType(cell);
        foreach (var n in neigh) {
            int dirX = n.x - cell.x;
            int dirY = n.y - cell.y;
            var r = GetConnectedCellsInDirection(n, dirX, dirY);
            if (r.Count > 1) result.AddRange(r);
        }

        return result.Count > 2 ? result : new List<Cell>();
    }

    public List<Cell> GetConnectedCellsInDirection (Cell cell, int x, int y) {
        int targetX = cell.x + x;
        int targetY = cell.y + y;
        var r = new List<Cell>();
        r.Add(cell);

        var tCell = GetCell(targetX, targetY);
        if (tCell != null && tCell.type == cell.type) {
            r.AddRange(GetConnectedCellsInDirection(tCell, x, y));
        }

        return r;
    }

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < GetRowsAmount(); i++) {
            output += "\n|";
            for (int j = 0; j < GetColumnsAmount(); j ++) {

                output += $"   {GetCell(i,j).type}   |";
            }
        }
        return output;
    }
}
