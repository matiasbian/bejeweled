using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    public Image sprite;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetCell (Cell cell) {
        sprite.color = cell.GetTypeColor();
        text.text = cell.type.ToString();
    }
}
