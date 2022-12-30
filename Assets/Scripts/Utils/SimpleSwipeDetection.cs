using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SimpleSwipeDetection : MonoBehaviour
{
    Vector2 touchPos;
    float tolerance = 0.4f;

    public static Action<Vector2Int, CellUI> onSwipe;


    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 0) {
            MouseDetection();
        } else {
            TouchDetection();
        }
    }

    void TouchDetection () {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began) {
            InputBegan(touch.position);
        }
        if (touch.phase == TouchPhase.Ended) {
            InputEnded(touch.position);
        }
    }

    void MouseDetection () {
        if (Input.GetMouseButtonDown(0)) {
            InputBegan(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0)) {
            InputEnded(Input.mousePosition);
        }
    }

    void InputBegan (Vector2 pos) {
        touchPos = pos;
    }

    void InputEnded (Vector2 pos) {
        if (Vector2.Distance(pos, touchPos) < 55) return;
            var dir = (pos - touchPos).normalized;
            CheckDirection(dir, touchPos);
    }

    void CheckDirection (Vector2 dir, Vector2 initialPos) {
        if (Mathf.Abs(dir.x) > tolerance && Mathf.Abs(dir.y) > tolerance) {
            Debug.LogWarning("Not a valid gesture");
            return;
        }

        var cellTouched = GetCellTouched(initialPos);

        if (!cellTouched) return;

        if (dir.x > 0.5f) {
            onSwipe?.Invoke(Vector2Int.right, cellTouched);
        } else if (dir.x < -0.5f) {
            onSwipe.Invoke(Vector2Int.left, cellTouched);
        } else if (dir.y > 0.5f) {
            onSwipe.Invoke(Vector2Int.up, cellTouched);
        } else if (dir.y < -0.5f) {
            onSwipe.Invoke(Vector2Int.down, cellTouched);
        }
    }

    CellUI GetCellTouched (Vector2 initialPos) {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = initialPos;

        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        CellUI cellTouched = null;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.transform.CompareTag("Cell")) {
                cellTouched = result.gameObject.GetComponent<CellUI>();
                break;
            }
        }

        return cellTouched;
    }
}
