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
    }

    public Cell GetCell (int x, int y) {
        if (x < 0 || x >= width || y < 0 || y >= height) return null;
        return grid[y,x];
    }

    public void ForEachElement(Action<Cell> action) {
        for (int i = 0; i < GetRowsAmount(); i++) {
            for (int j = 0; j < GetColumnsAmount(); j ++) {
                action(GetCell(i,j));
            }
        }
    }

    List<Cell> GetNeighboringCells (int x, int y) {
        List<Cell> cells = new List<Cell>();
        
        cells.AddIfNotNull(GetCell(x - 1, y));
        cells.AddIfNotNull(GetCell(x + 1, y));
        cells.AddIfNotNull(GetCell(x, y - 1));
        cells.AddIfNotNull(GetCell(x, y + 1));
        return cells;
    }

    public List<Cell> GetNeighboringCellsOfSameType (int x, int y) {
        Cell cell = GetCell(x,y);
        var cells = GetNeighboringCells(x,y);
        return GetNeighboringCells(x,y).FindAll(nCell => nCell.type == cell.type);
    }

    public HashSet<Cell> GetAllConectedCells (int x, int y, HashSet<Cell> connected = null) {
        Cell cell = GetCell(x,y);
        if (connected.Contains(cell)) return new HashSet<Cell>();

        connected.Add(cell);
        var nei = GetNeighboringCellsOfSameType(cell.x, cell.y);

        foreach (var c in nei) {
            var result = GetAllConectedCells(c.x, c.y, connected);
            connected.UnionWith(result);
        }

        return connected;
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
