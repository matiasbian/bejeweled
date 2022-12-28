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
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCell (Cell cell) {
        sprite.color = cell.GetTypeColor();
        this.cell = cell;
        UpdateText(cell);
        cell.onUpdateCell += UpdateText; 
        cell.onUpdateCell += UpdateColor;
        cell.onHide += HideCell; 
        cell.SetCellUI(this);
        cell.newCell += NewCell;
    }

    void UpdateText (Cell cell) {
        text.text = cell.type.ToString() + " x " + cell.y + " y " + cell.x;
    }

    void UpdateColor (Cell cell) {
        if (cell.disabled) {
            sprite.color = Color.white;
            return;
        }
        sprite.color = cell.colors[(int) cell.type];
    }

    void HideCell (Cell cell) {
        //sprite.color = Color.clear;//new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f);
        animator.SetTrigger("Hide");
        cell.hidden = false;
    }

    public void NewCell(Cell cell) {
        animator.SetTrigger("PopUp");
    }

    public void Click () {
        onCellClicked?.Invoke(this);
    }
    
    public void SelectCell () {
        sprite.color = Color.white;
    }

    public void UnselectCell () {
        UpdateColor(cell);
    }

    public Cell GetCell() {
        return cell;
    }
}
