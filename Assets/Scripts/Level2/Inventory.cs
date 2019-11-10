using UnityEngine;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour, IHasChanged
{
    private void Start()
    {
        HasChanged();
    }
    public void HasChanged()
    {
        
    }
}

namespace UnityEngine.EventSystems{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}