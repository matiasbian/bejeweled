using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class CellUI : MonoBehaviour
{
    public Image sprite;
    public TextMeshProUGUI text;
    public static Action<Cell> onCellClicked;
    Cell cell;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetCell (Cell cell) {
        sprite.color = cell.GetTypeColor();
        text.text = cell.type.ToString() + " x " + cell.x + " y " + cell.y;
        this.cell = cell;
    }

    public void Click () {
        onCellClicked?.Invoke(cell);
    }
}
