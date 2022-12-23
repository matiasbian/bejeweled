using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        return grid[x,y];
    }

    public void ForEachElement(Action<Cell> action) {
        for (int i = 0; i < GetRowsAmount(); i++) {
            for (int j = 0; j < GetColumnsAmount(); j ++) {
                action(GetCell(i,j));
            }
        }
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
