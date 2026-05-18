using UnityEngine;
using UnityEngine.Events;
public class DoorDestruction : MonoBehaviour
{
    [SerializeField] private int m_switchcount;
    [SerializeField] private int m_volatileswitchcount;
    private int m_switchestriggered;
    private int m_volatileswitchestriggered;
    public UnityEvent VolatileSwitchesTriggered;
    /// <summary>
    /// called when switches are triggered, bool is passed in to determine if volatile switch
    /// </summary>
    /// <param name="m_isvolatile"></param>
    public void DoorDestructionHandler(bool m_isvolatile)
    {
        m_switchestriggered++;
        if (m_isvolatile == true)
        {
            m_volatileswitchestriggered++;
            // if volatile switches are equal to volatile switch count for that room triggers event that permanently sets them to red.
            if (m_volatileswitchestriggered == m_volatileswitchcount)
            {
                VolatileSwitchesTriggered?.Invoke();
            }
        }
        // if all switches in a room triggered the door is destroyed
        if (m_switchcount == m_switchestriggered)
        {
            gameObject.SetActive(false);
        }  
    }
    // called via event when not all volatile switches in a scene are activated, to reset switch count back to how it was before volatile switches were triggered
    public void DoorRemoveCount()
    {
        m_volatileswitchestriggered--;
        m_switchestriggered--;
    }
}
