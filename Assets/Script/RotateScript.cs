using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform obstacle;
    public RectTransform canvasTransform;
    Vector2 startPos;
    float scale;
    public float angle = 45f;
    int counter;
    void Start ()
    {
        scale = canvasTransform.rect.width/Screen.width;
        obstacle.eulerAngles = Vector3.forward*angle;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startPos = eventData.position;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data) {
        SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data) {



        angle -= (data.delta.y+data.delta.x) * scale;
        //if (Mathf.Abs(angle)>90) angle = 90 * Mathf.Sign(angle);
        obstacle.eulerAngles = Vector3.forward*angle;
    }

    public void OnEndDrag(PointerEventData data) {
        SetDraggedPosition(data);
    }
  
}
