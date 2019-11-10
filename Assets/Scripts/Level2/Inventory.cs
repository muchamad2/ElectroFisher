using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    private List<DragHandler> tools = new List<DragHandler>();
    private void Start()
    {
        HasChanged();
    }
    public void HasChanged()
    {
        EraseList();
        foreach (Transform slotItem in slots)
        {

            Slot slot = slotItem.GetComponent<Slot>();
            GameObject tool = slot.item;
            if (tool)
            {
                DragHandler handler = tool.GetComponent<DragHandler>();
                if (slot.getId == handler.getId)
                    handler.isCorrect = true;
                else
                    handler.isCorrect = false;
                tools.Add(handler);
                handler.isChecked = true;
            }
        }
        
        TrialManager.Instance.CheckTools(tools);
    }
    public void EraseList()
    {
        tools = new List<DragHandler>();
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}