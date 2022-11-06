using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_and_Drop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private RectTransform transform;
    private Vector2 start_position;

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.anchoredPosition != start_position)
        {
            transform.anchoredPosition = start_position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    void Start()
    {
        transform = GetComponent<RectTransform>();
        start_position = transform.anchoredPosition;
    }
}
