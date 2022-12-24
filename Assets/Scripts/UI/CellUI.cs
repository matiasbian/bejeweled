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
    public static Action<CellUI> onCellClicked;
    Cell cell;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetCell (Cell cell) {
        sprite.color = cell.GetTypeColor();
        this.cell = cell;
        UpdateText(cell.x, cell.y);
        cell.onUpdatePos += UpdateText; 
    }

    void UpdateText (int x, int y) {
        text.text = cell.type.ToString() + " x " + x + " y " + y;
    }

    public void Click () {
        onCellClicked?.Invoke(this);
    }

    public Cell GetCell() {
        return cell;
    }
}
