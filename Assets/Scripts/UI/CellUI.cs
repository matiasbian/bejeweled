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
        UpdateText(cell);
        cell.onUpdateCell += UpdateText; 
        cell.onUpdateCell += UpdateColor; 
    }

    void UpdateText (Cell cell) {
        text.text = cell.type.ToString() + " x " + cell.y + " y " + cell.x;
    }

    void UpdateColor (Cell cell) {
        sprite.color = cell.colors[(int) cell.type];
    }

    public void Click () {
        onCellClicked?.Invoke(this);
    }

    public Cell GetCell() {
        return cell;
    }
}
