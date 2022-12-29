using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCellSizeDynamic : MonoBehaviour
{
    public float offset;
    // Start is called before the first frame update
    void Update()
    {
        var width = this.gameObject.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / 2 * offset, width / 2 * offset);
        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

}
