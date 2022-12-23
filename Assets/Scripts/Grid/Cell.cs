using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    const int TYPES_LENGHT = 5;

    public enum Type {A, B, C, D, E}
    public Color[] colors = new Color[5] {Color.red, Color.blue, Color.yellow, Color.magenta, Color.green};
    public Type type;
    
    public Cell (Type type) {
        this.type = type;
    }

    public static Cell GetRandomCell () {
        int typeIndex = Random.Range(0,TYPES_LENGHT);
        Cell cell = new Cell((Cell.Type) typeIndex);
        return cell;
    }

    public Color GetTypeColor () {
        return colors[(int) type];
    }
}
