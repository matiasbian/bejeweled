using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Cell
{
    const int TYPES_LENGHT = 5;

    public enum Type {A, B, C, D, E, Disabled}
    public Color[] colors = new Color[6] {Color.red, Color.blue, Color.yellow, Color.magenta, Color.green, Color.grey};
    public Type type;
    public int x;
    public int y;

    public Action<Cell> onUpdateCell;
    
    public Cell (Type type, int x, int y) {
        this.type = type;
        this.x = x;
        this.y = y;
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
        this.type = Type.Disabled;
        onUpdateCell?.Invoke(this);
    }

    public Color GetTypeColor () {
        return colors[(int) type];
    }

    public override string ToString()
    {
        return $"x: {x} y: {y}";
    }
}
