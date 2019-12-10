using UnityEngine;

public class FisherAnimEvent : MonoBehaviour
{
    private bool areaEvent = false;
    public bool AreaEvent{
        get{
            return areaEvent;
        }
    }
    public void ActiveAreaLaunch() { 
        areaEvent = true;
    }
    public void DeactiveAreaLunch() { 
        areaEvent = false;
    }
}