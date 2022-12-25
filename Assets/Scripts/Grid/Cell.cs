using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Cell
{
    const int TYPES_LENGHT = 5;

    public enum Type {A, B, C, D, E}
    public Color[] colors = new Color[5] {Color.red, Color.blue, Color.yellow, Color.magenta, Color.green};
    public Type type;
    public int x;
    public int y;

    public bool disabled;
    public bool hidden;

    public CellUI ui;

    public Action<Cell> onUpdateCell;
    
    public Cell (Type type, int x, int y) {
        this.type = type;
        this.x = x;
        this.y = y;
    }

    public void SetCellUI (CellUI cellUI) {
        ui = cellUI;
    }

    public static Cell GetRandomCell (int x, int y) {
        int typeIndex = UnityEngine.Random.Range(0,TYPES_LENGHT);
        Cell cell = new Cell((Cell.Type) typeIndex, x, y);
        return cell;
    }

    public void UpdatePos (int x, int y) {
        this.x = x;
        this.y = y;
        onUpdateCell?.Invoke(this);
    }

    public void DisableCell () {
        disabled = true;
        onUpdateCell?.Invoke(this);
    }

    public void EnableCell () {
        disabled = false;
        onUpdateCell?.Invoke(this);
    }

    public void HideCell () {
        hidden = true;
        onUpdateCell?.Invoke(this);
    }

    public void ResetCell () {
        hidden = false;
        disabled = false;
        onUpdateCell?.Invoke(this);
    }

    public Color GetTypeColor () {
        return colors[(int) type];
    }

    public void CreateNewOne () {
        var random = Cell.GetRandomCell(x, y);
        this.type = random.type;
        this.hidden = false;
        onUpdateCell?.Invoke(this);
    }


    public override string ToString()
    {
        return $"x: {y} y: {x}";
    }
       
}
