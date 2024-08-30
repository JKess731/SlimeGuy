using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit?.Invoke();
    }
}
