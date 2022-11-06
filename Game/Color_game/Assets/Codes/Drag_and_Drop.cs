using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_and_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private RectTransform R_transform;
    private Vector2 start_position;

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        R_transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (R_transform.anchoredPosition != start_position)
        {
            R_transform.anchoredPosition = start_position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    void Start()
    {
        R_transform = GetComponent<RectTransform>();
        start_position = R_transform.anchoredPosition;
    }
}
