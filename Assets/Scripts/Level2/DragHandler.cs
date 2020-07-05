using UnityEngine;
using UnityEngine.EventSystems;
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeginDragged;
    [SerializeField] private int _id;
    public int getId
    {
        get
        {
            return _id;
        }
    }
    public bool isChecked = false;
    public bool isCorrect = false;
    public bool isDragable = true;
    Vector3 startPosition;
    Transform startParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            itemBeginDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            Vector3 noZvalue = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            noZvalue.z = 0;
            transform.position = noZvalue;
            startParent.GetComponent<Slot>().ImageActive(true);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            itemBeginDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;

            if (transform.parent == startParent)
            {
                transform.position = startPosition;
                startParent.GetComponent<Slot>().ImageActive(false);
                
                if(gameObject.TryGetComponent(out VideoExperimental component)){
                    component.invokeVideo();
                }
            }
        }
    }
}