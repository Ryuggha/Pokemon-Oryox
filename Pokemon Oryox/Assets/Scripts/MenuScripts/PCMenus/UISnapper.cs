using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UISnapper : MonoBehaviour, IDropHandler
{
    public int index;
    public TMP_Text text;

    public void OnDrop(PointerEventData eventData)
    {
        var childCount = 1;
        if (text != null) childCount = 2;
        if (transform.childCount == childCount)
        {
            Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
            draggable.parentAfterDrag = transform;
        }
        
    }
}
