using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float Vertical = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vertical = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vertical = 0;
    }


}
