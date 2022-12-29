using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class CellUI : MonoBehaviour
{
    public Image sprite;
    public Image icon;
    public TextMeshProUGUI text;
    public static Action<CellUI> onCellClicked;
    public Outline outline;

    Cell cell;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCell (Cell cell) {
        this.cell = cell;
        cell.onUpdateCell += UpdateColor;
        cell.onHide += HideCell; 
        cell.SetCellUI(this);
        cell.newCell += NewCell;
        UpdateColor(cell);
    }

    void UpdateColor (Cell cell) {
        if (cell.disabled) {
            sprite.color = Color.white;
            return;
        }

        if (cell.error) {
            sprite.color = Color.red;
            return;
        }

        int index = (int) cell.type;
        sprite.color = cell.GetTypeColor();
        icon.sprite = cell.GetTypeSprite();
    }

    void HideCell (Cell cell) {
        animator.SetTrigger("Hide");
        cell.hidden = false;
    }

    public void ShowError () {
        cell.Error();
    }

    public void ClearError () {
        cell.ClearError();
    }

    public void NewCell(Cell cell) {
        animator.SetTrigger("PopUp");
    }

    public void Click () {
        onCellClicked?.Invoke(this);
    }
    
    public void SelectCell () {
        outline.enabled = true;
    }

    public void UnselectCell () {
        outline.enabled = false;
    }

    public Cell GetCell() {
        return cell;
    }
}
