using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Grid
{
    public int height;
    public int width;
    Cell[,] grid;

    public Action<Cell> cellUpdated;
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

    public Cell GetOverCell (Cell cell) {
        return GetCell(cell.x -1, cell.y);
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
        cellUpdated?.Invoke(b);
        SetValue(bx, by, a);
        cellUpdated?.Invoke(a);

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
        
        return GetNeighboringCells(a).Contains(b);
    }

    public List<Cell> GetNeighboringCellsOfSameType (Cell cell) {
        var cells = GetNeighboringCells(cell);
        return GetNeighboringCells(cell).FindAll(nCell => nCell.type == cell.type);
    }

    public List<Cell> GetConnectedLines (Cell cell) {
        var result = new HashSet<Cell>();
        result.Add(cell);
        var neigh = GetNeighboringCellsOfSameType(cell);
        foreach (var n in neigh) {
            int dirX = Mathf.Clamp(n.x - cell.x, -1, 1);
            int dirY = Mathf.Clamp(n.y - cell.y, -1, 1);
            //check for connected cells in both directions
            var r = GetConnectedCellsInDirection(n, dirX, dirY, cell.type);
            var r2 = GetConnectedCellsInDirection(n, -dirX , -dirY , cell.type, false);
            //add to result list the conencted cells
            HashSet<Cell> union = new HashSet<Cell>();
            union.UnionWith(r);
            union.UnionWith(r2);
            if (union.Count > 2) {
                result.UnionWith(union);
            }
        }
        //if there's a connection, return it, otherwise return a no connections list
        return result.Count > 2 ? result.ToList() : new List<Cell>();
    }

    public List<Cell> GetConnectedCellsInDirection (Cell cell, int x, int y, Cell.Type type, bool addCell = true) {
        int targetX = cell.x + x;
        int targetY = cell.y + y;
        var r = new List<Cell>();
        
        if (type != cell.type) return r;
        if (addCell) r.Add(cell);

        var tCell = GetCell(targetX, targetY);
        if (tCell != null && tCell.type == cell.type) {
            r.AddRange(GetConnectedCellsInDirection(tCell, x, y, type));
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
