using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject image;
    [SerializeField] int _id;

    public bool areaDrop = false;
    public int getId
    {
        get
        {
            return _id;
        }
    }
    public GameObject item
    {
        get
        {
            if (transform.childCount > 1)
            {
                return transform.GetChild(1).gameObject;
            }
            return null;
        }
    }
    public void ImageActive(bool state)
    {
        if (image != null)
        {
            image.SetActive(state);
            //gameObject.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!areaDrop)
        {
            if (image != null)
                image.SetActive(false);
            if (!item)
            {
                DragHandler.itemBeginDragged.transform.SetParent(transform);
                ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
                if (item)
                {
                    if (getId == item.GetComponent<DragHandler>().getId)
                        TrialManager.Instance.Correction(true);
                    else
                        TrialManager.Instance.Correction(false);
                }
            }
        }
    }
}