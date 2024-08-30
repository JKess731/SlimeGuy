using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ListViewManager : EditorWindow
{
   [SerializeField] RectTransform imageRectTransform;
   [SerializeField] Canvas canvas;

    void Update()
    {
        if (IsMouseOverRectTransform(imageRectTransform, canvas))
        {
            Debug.Log("Mouse is over the image.");
        }
    }

    bool IsMouseOverRectTransform(RectTransform rectTransform, Canvas canvas)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.worldCamera, out localMousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }
}
