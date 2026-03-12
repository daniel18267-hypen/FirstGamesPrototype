using UnityEngine;
using UnityEngine.Events;
public class VolatileSwitch : MonoBehaviour
{
    public UnityEvent TriggerDoorVolatile;
    public void TriggerVolatileSwitch()
    {
        TriggerDoorVolatile?.Invoke();
    }
}
